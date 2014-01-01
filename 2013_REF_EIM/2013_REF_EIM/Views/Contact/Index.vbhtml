@ModelType IEnumerable(Of Contact)
@Code
    ViewData("Title") = "Index"
End Code

<h2>Index</h2>

<p>
    @Html.ActionLink("Create New", "Create")
</p>
<table class="table">
    <tr>
        <th>
            @Html.DisplayNameFor(Function(model) model.FirstName)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.LastName)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Title)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.AddDate)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.ModifiedDate)
        </th>
        <th></th>
    </tr>

@For Each item In Model
    @<tr>
        <td>
            @Html.DisplayFor(Function(modelItem) item.FirstName)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.LastName)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Title)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.AddDate)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.ModifiedDate)
        </td>
        <td>
            @Html.ActionLink("Edit", "Edit", New With {.id = item.ContactID }) |
            @Html.ActionLink("Details", "Details", New With {.id = item.ContactID }) |
            @Html.ActionLink("Delete", "Delete", New With {.id = item.ContactID })
        </td>
    </tr>
Next

</table>
