$(function () {
    let today = new Date();
    if (today.getDate() >= 10) {
        $('#fromDate').val((today.getMonth() + 1) + "/" + today.getDate() + "/" + today.getFullYear());
    } else {
        $('#fromDate').val((today.getMonth() + 1) + "/" + '0' + today.getDate() + "/" + today.getFullYear());
    }
    let loadDate = $('#fromDate').val();
    function dateInDivs(date, type) {
        date = date.split('/');
        $('#' + type + 'Month').text(date[0]);
        $('#' + type + 'Day').text(date[1]);
        $('#' + type + 'Year').text(date[2]);
    }

    function dateInDivsFrom(date) {
        dateInDivs(date, 'from');
    }

    function dateInDivsTo(date) {
        dateInDivs(date, 'to');
    }

    dateInDivsFrom($('#fromDate').val());
    today.setDate(today.getDate() + 1);
    if (today.getDate() >= 10) {
        $('#toDate').val((today.getMonth() + 1) + "/" + today.getDate() + "/" + today.getFullYear());
    } else {
        $('#toDate').val((today.getMonth() + 1) + "/" + '0' + today.getDate() + "/" + today.getFullYear());
    }
    dateInDivsTo($('#toDate').val());
    $(function () {
        $('#fromDate').datepicker("option", "onSelect", dateInDivsFrom);
        $('#toDate').datepicker("option", "onSelect", dateInDivsTo);
    });
});