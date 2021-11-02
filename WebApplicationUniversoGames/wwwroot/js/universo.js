$(document).ready(
    function () {
        $("#btn_collapse_menu").hide();
        $("#formnewssearch").hide();
    }
);

function ShowAndHide() {
    var navcollapse = document.getElementById('navbarNav');
    var x = document.getElementById('formnewssearch');
    if (x.style.display == 'none') {
        if (navcollapse.style.display == 'none') {
            x.style.marginTop = '35%';
        } else {
            x.style.marginTop = '15%';
        }
    } else {
        x.style.display = 'none';
    }
}