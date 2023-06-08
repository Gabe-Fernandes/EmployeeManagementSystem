$(document).ready(function () {
  $("#searchBtn").on("click", () => {
    $("#searchLbl").text(`Showing search results for: "${$("#searchFirstName").val()} ${$("#searchLastName").val()}"`);
    if ($("#searchFirstName").val() === "" &&
      $("#searchLastName").val() === "") {
      $("#searchLbl").text("Showing all users");
    }
  });

  $("#removeUserCloseBtn").on("click", () => {
    ToggleModal($("#manageUsers"), $("#removeUserModal"), closeModal);
  });
  $("#removeUserCancelBtn").on("click", () => {
    ToggleModal($("#manageUsers"), $("#removeUserModal"), closeModal);
  });
  $("#remove1").on("click", () => {
    ToggleModal($("#manageUsers"), $("#removeUserModal"), openModal);
  });
  $("#remove2").on("click", () => {
    ToggleModal($("#manageUsers"), $("#removeUserModal"), openModal);
  });
});
