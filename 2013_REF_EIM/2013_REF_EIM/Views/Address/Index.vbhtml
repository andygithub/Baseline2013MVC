@ModelType IEnumerable(Of Address)
@Code
    ViewData("Title") = "Index"
End Code

<h2>Index</h2>

<table class="table">
    <tr>
        <th>
            @Html.DisplayNameFor(Function(model) model.AddressType)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.City)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.CountryRegion)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.PostalCode)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.ModifiedDate)
        </th>
    </tr>

@For Each item In Model
    @<tr>
        <td>
            @Html.DisplayFor(Function(modelItem) item.AddressType)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.City)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.CountryRegion)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.PostalCode)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.ModifiedDate)
        </td>
    </tr>
Next

</table>
