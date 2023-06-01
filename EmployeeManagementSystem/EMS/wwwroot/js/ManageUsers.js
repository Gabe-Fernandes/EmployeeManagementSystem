$(document).ready(function () {
  const openModal = "O";
  const closeModal = "C";

  function ToggleModal(main, modal, direction) {
    if (direction === openModal) {
      main.addClass("unclickable");
      modal.removeClass("hide");
    }
    else if (direction === closeModal) {
      main.removeClass("unclickable");
      modal.addClass("hide");
    }
  }

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
