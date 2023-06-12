$(function () {
  HighlightCurrentNavBtn($("#manageUsersNavBtn"));

  const allInputNames = ["SearchFirstName", "SearchLastName"];
  let allInputIDs = [];
  let allInputFields = [];
  let allErrIDs = [];

  for (let i = 0; i < allInputNames.length; i++) {
    allInputIDs.push(`manageUsers${allInputNames[i]}`);
    allInputFields.push($(`#${allInputIDs[i]}`));
    allErrIDs.push(`${allInputIDs[i]}Err`);
  }
  $("#manageUsersSearchForm").on("submit", (evt) => {
    // validation
    let errorExists = false;
    if (AllCharsValid(allInputFields, allErrIDs) === false) {
      errorExists = true;
    }
    if (ValidCharLimit(allInputFields, allErrIDs, 30) === false) {
      errorExists = true;
    }

    if (errorExists) {
      $("#searchLbl").text("error");
      evt.preventDefault()
    }
  });

  // Clear Error Handling Events

  for (let i = 0; i < allInputIDs.length; i++) {
    $(`#${allInputIDs[i]}`).on("input", () => {
      HideError(allInputIDs[i], allErrIDs[i]);
    });
  }

  // Modal events

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
