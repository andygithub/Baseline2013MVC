// Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Data.Common;
using System.IO;
using EFProviderWrapperToolkit;

namespace EFTracingProvider
{
    /// <summary>
    /// Wrapper <see cref="DbConnection"/> which traces all executed commands.
    /// </summary>
    public class EFTracingConnection : DbConnectionWrapper
    {
        private static object consoleLockObject = new object();

        /// <summary>
        /// Initializes a new instance of the EFTracingConnection class.
        /// </summary>
        public EFTracingConnection()
        {
            this.AddDefaultListenersFromConfiguration();
        }

        /// <summary>
        /// Initializes a new instance of the EFTracingConnection class.
        /// </summary>
        /// <param name="wrappedConnection">The wrapped connection.</param>
        public EFTracingConnection(DbConnection wrappedConnection)
        {
            this.WrappedConnection = wrappedConnection;
            this.AddDefaultListenersFromConfiguration();
        }

        /// <summary>
        /// Occurs when database command is executing.
        /// </summary>
        public event EventHandler<CommandExecutionEventArgs> CommandExecuting;

        /// <summary>
        /// Occurs when database command has finished execution.
        /// </summary>
        public event EventHandler<CommandExecutionEventArgs> CommandFinished;

        /// <summary>
        /// Occurs when database command execution has failed.
        /// </summary>
        public event EventHandler<CommandExecutionEventArgs> CommandFailed;

        /// <summary>
        /// Gets the <see cref="T:System.Data.Common.DbProviderFactory"/> for this <see cref="T:System.Data.Common.DbConnection"/>.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// A <see cref="T:System.Data.Common.DbProviderFactory"/>.
        /// </returns>
        protected override DbProviderFactory DbProviderFactory
        {
            get { return EFTracingProviderFactory.Instance; }
        }

        /// <summary>
        /// Gets the name of the default wrapped provider.
        /// </summary>
        /// <returns>Name of the default wrapped provider.</returns>
        protected override string DefaultWrappedProviderName
        {
            get { return EFTracingProviderConfiguration.DefaultWrappedProvider; }
        }

        internal void RaiseExecuting(CommandExecutionEventArgs e)
        {
            if (this.CommandExecuting != null)
            {
                this.CommandExecuting(this, e);
            }
        }

        internal void RaiseFinished(CommandExecutionEventArgs e)
        {
            if (this.CommandFinished != null)
            {
                this.CommandFinished(this, e);
            }
        }

        internal void RaiseFailed(CommandExecutionEventArgs e)
        {
            if (this.CommandFailed != null)
            {
                this.CommandFailed(this, e);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "Component is non-localizable")]
        private void AddDefaultListenersFromConfiguration()
        {
            if (EFTracingProviderConfiguration.LogToConsole)
            {
                this.CommandExecuting += delegate(object sender, CommandExecutionEventArgs e)
                {
                    lock (consoleLockObject)
                    {
                        var oldColor = Console.ForegroundColor;

                        try
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("#{0} Running {1}:", e.CommandId, e.Method);
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine(e.ToTraceString());
                            //The console writeline for mvc projects does not display in the output window, so duplicating the relevant console calls to Diagnostic.Writeline
                            System.Diagnostics.Debug.WriteLine("#{0} Running {1}:", e.CommandId, e.Method);
                            System.Diagnostics.Debug.WriteLine(e.ToTraceString());
                            //Rather than wire up the mini profiler as a seperate provider the miniprofiler dependency is taken on in this assembly and items are populated.  
                            //This should be broken out into a serpate "logging" configuration and event section, other option would be to use the existing logaction interface
                            //StackExchange.Profiling.MiniProfiler.
                            //StackExchange.Profiling.MiniProfiler.Current.Step("Running-" + e.Method.ToString() + MiniProfiler.Settings.SqlFormatter.FormatSql( e.ToTraceString()));
                            //StackExchange.Profiling.SqlProfiler profiler = new StackExchange.Profiling.SqlProfiler(StackExchange.Profiling.MiniProfiler.Current);
                            //profiler.ExecuteStart(e.Command, StackExchange.Profiling.Data.ExecuteType.Reader);
                        }
                        finally
                        {
                            Console.ForegroundColor = oldColor;
                        }
                    }
                };

                this.CommandFinished += delegate(object sender, CommandExecutionEventArgs e)
                {
                    lock (consoleLockObject)
                    {
                        var oldColor = Console.ForegroundColor;

                        try
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("#{0} Command completed in {1}", e.CommandId, e.Duration);
                            //The console writeline for mvc projects does not display in the output window, so duplicating the relevant console calls to Diagnostic.Writeline
                            System.Diagnostics.Debug.WriteLine("#{0} Command completed in {1}", e.CommandId, e.Duration);

                            //var id = Tuple.Create((object)e.Command, StackExchange.Profiling.Data.SqlExecuteType.Reader);
                            //StackExchange.Profiling.SqlProfiler profiler = new StackExchange.Profiling.SqlProfiler(StackExchange.Profiling.MiniProfiler.Current);
                            //profiler.ExecuteFinish(e.Command,  StackExchange.Profiling.Data.SqlExecuteType.Reader);
                        }
                        finally
                        {
                            Console.ForegroundColor = oldColor;
                        }
                    }
                };
                this.CommandFailed += delegate(object sender, CommandExecutionEventArgs e)
                {
                    lock (consoleLockObject)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("#{0} Command failed {1}", e.CommandId, e.Result);
                        //The console writeline for mvc projects does not display in the output window, so duplicating the relevant console calls to Diagnostic.Writeline
                        System.Diagnostics.Debug.WriteLine("#{0} Command failed {1}", e.CommandId, e.Result);

                        //var id = Tuple.Create((object)e.Command, StackExchange.Profiling.Data.SqlExecuteType.Reader);
                        //StackExchange.Profiling.SqlProfiler profiler = new StackExchange.Profiling.SqlProfiler(StackExchange.Profiling.MiniProfiler.Current);
                        //profiler.ExecuteFinish(e.Command, StackExchange.Profiling.Data.SqlExecuteType.Reader);
                    }
                };
            }

            string logFile = EFTracingProviderConfiguration.LogToFile;

            if (logFile != null)
            {
                this.CommandExecuting += delegate(object sender, CommandExecutionEventArgs e)
                {
                    File.AppendAllText(logFile, e.ToTraceString() + "\r\n\r\n");
                };
            }

            var logAction = EFTracingProviderConfiguration.LogAction;
            if (logAction != null)
            {
                this.CommandExecuting += delegate(object sender, CommandExecutionEventArgs e)
                {
                    logAction(e);
                };

                this.CommandFinished += delegate(object sender, CommandExecutionEventArgs e)
                {
                    logAction(e);
                };

                this.CommandFailed += delegate(object sender, CommandExecutionEventArgs e)
                {
                    logAction(e);
                };
            }
        }
    }
}
