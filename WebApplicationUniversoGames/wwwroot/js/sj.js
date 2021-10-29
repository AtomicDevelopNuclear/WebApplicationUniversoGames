$(document).ready(
    function () {
        $('#searchbar').hide()
        $('#btnbar').hide()
    }
);

function ShowAndHide() {
    var x = document.getElementById('searchbar');
    var z = document.getElementById('btnbar');
    $('#searchbar').val('');
    if (x.style.display == 'none' && z.style.display == 'none') {
        x.style.display = 'block';
        z.style.display = 'block';
    } else {
        x.style.display = 'none';
        z.style.display = 'none';
    }
};