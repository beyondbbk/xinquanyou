function NomarlMsg(msg) {
    $("#detailmodal #tbbody").html(msg);
    $("#detailmodal .modal-dialog").css("min-width", "800px");
    $(".table tbody tr td").css("vertical-align","middle");
    $("#detailmodal").modal("show");
}