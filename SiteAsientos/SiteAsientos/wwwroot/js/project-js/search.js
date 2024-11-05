//Filtro de lista por medio de buscador
$('#txtSearch').keyup(function ()
{
    var value = $(this).val();
    $('tbody tr').each(function ()
    {
        if ($(this).text().search(new RegExp(value, "i")) < 0) {
            $(this).fadeOut();
        }
        else
        {
            $(this).show();
        }
    })
})