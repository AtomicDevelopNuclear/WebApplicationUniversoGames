$(document).ready(
    function () {
        $("#formnewssearch").hide();
    }
);

function ShowAndHide() {
    var x = document.getElementById('formnewssearch');
    if (x.style.display == 'none') {
        x.style.display = 'block';
    } else {
        x.style.display = 'none';
    }
}