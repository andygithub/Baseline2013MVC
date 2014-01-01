@Code
    ViewData("Title") = "Server Error"
End Code

<br />
<div class="alert alert-danger">
    <h2>Unhandled Server Error</h2>
    <hr />
    <p>An unexpected error has taken place.  If you feel that you have reached this page in error please contact the helpdesk.  To continue using the use the link below or the menu navigation. </p>
    <br /><p>
        @Html.ActionLink("Return to Previous Page", "Index", "Home", Nothing, New With {.class = "btn btn-primary"})
    <a class="btn btn-info" href="http://go.microsoft.com/fwlink/?LinkId=301867">Contact Helpdesk</a>
</p>
<br />
</div>

