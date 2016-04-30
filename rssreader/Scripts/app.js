function createAlert(message, type) {
    var alert = $("<div></div>").addClass("alert alert-" + type).html(message);
    window.setTimeout(function () { 
        alert.alert('close');
    },5000);
    $(".alerts").append(alert);
}

function addFeedLoader() {
    $("#add-feed-loader").removeClass("hidden");
}
function addFilterLoader() {
    $("#filter-loader").removeClass("hidden");
}
function addDeleteLoader() {
    $("#delete-loader").removeClass("hidden");
}
function showRefreshLoader() {
    $("#refresh i").addClass("fa-spin");
}

$(document).ready(function () {

    $(document).on("click", "#check-all", function () {

        var btn = $(this);

        if (btn.data("checked")) {
            $(".feeds input:checkbox").removeAttr("checked");
            btn.data("checked", false);
            btn.text("Vybrat vše");
        } else {
            $(".feeds input:checkbox").prop("checked",true);
            btn.data("checked", true);
            btn.text("Odvybrat vše");
        }
    });
});

$.datepicker.regional['cs'] = {
    closeText: 'Cerrar',
    prevText: 'Předchozí',
    nextText: 'Další',
    currentText: 'Hoy',
    monthNames: ['Leden', 'Únor', 'Březen', 'Duben', 'Květen', 'Červen', 'Červenec', 'Srpen', 'Září', 'Říjen', 'Listopad', 'Prosinec'],
    monthNamesShort: ['Le', 'Ún', 'Bř', 'Du', 'Kv', 'Čn', 'Čc', 'Sr', 'Zá', 'Ří', 'Li', 'Pr'],
    dayNames: ['Neděle', 'Pondělí', 'Úterý', 'Středa', 'Čtvrtek', 'Pátek', 'Sobota'],
    dayNamesShort: ['Ne', 'Po', 'Út', 'St', 'Čt', 'Pá', 'So', ],
    dayNamesMin: ['Ne', 'Po', 'Út', 'St', 'Čt', 'Pá', 'So'],
    weekHeader: 'Sm',
    dateFormat: 'dd.mm.yy',
    firstDay: 1,
    isRTL: false,
    showMonthAfterYear: false,
    yearSuffix: ''
};