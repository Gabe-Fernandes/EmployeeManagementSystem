$(function () {
  // Populate input values with booleans
  $("#approveBtn").on("click", () => {
    $("#isApprovedToSendToServer").val(true);
  });
  $("#rejectBtn").on("click", () => {
    $("#isApprovedToSendToServer").val(false);
  });
});
