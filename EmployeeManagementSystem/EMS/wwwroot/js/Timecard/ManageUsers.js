$(function () {
  HighlightCurrentNavBtn($("#manageUsersNavBtn"));

  const allInputNames = ["SearchName"];
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


  // TH sorting events

  let thFirstNameInOrder = true;
  let thLastNameInOrder = true;
  let thUpToDateInOrder = true;

  $("#thFirstName").on("click", () => {
    thFirstNameInOrder = thSortEvent("manageUsersTbody", thFirstNameInOrder, "manageUsersTR", "sortFirstName", alphabeticallyFirst);
  });
  $("#thLastName").on("click", () => {
    thLastNameInOrder = thSortEvent("manageUsersTbody", thLastNameInOrder, "manageUsersTR", "sortLastName", alphabeticallyFirst);
  });
  $("#thUpToDate").on("click", () => {
    thUpToDateInOrder = thSortEvent("manageUsersTbody", thUpToDateInOrder, "manageUsersTR", "sortUpToDate", alphabeticallyFirst);
  });

  // Modal events



  $("#removeUserCloseBtn").on("click", () => {
    ToggleModal($("#manageUsers"), $("#removeUserModal"), closeModal);

    // get the ID of the label currently in the modal
    const id = $("#removeUserModalBackground").children().attr("id");
    const index = id.replace("moveLabel_", "");
    const wrapStr = "#wrapHoldingMovableElements_" + index;

    MoveElements(index, wrapStr, wrapStr);
  });

  $("#removeUserCancelBtn").on("click", () => {
    ToggleModal($("#manageUsers"), $("#removeUserModal"), closeModal);

    // get the ID of the label currently in the modal
    const id = $("#removeUserModalBackground").children().attr("id");
    const index = id.replace("moveLabel_", "");
    const wrapStr = "#wrapHoldingMovableElements_" + index;

    MoveElements(index, wrapStr, wrapStr);
  });

  $(".removeAppUserBtn").on("click", () => {
    ToggleModal($("#manageUsers"), $("#removeUserModal"), openModal);

    const index = document.activeElement.id;

    MoveElements(index, "#removeUserModalBackground", "#removeUserModalOptionsWrap");
  });

  function MoveElements(index, labelDestination, btnDestination) {
    const labelStr = "#moveLabel_" + index;
    const btnStr = "#moveBtn_" + index;
    const inputVal = "#inputVal_" + index;
    $(labelDestination).prepend($(labelStr));
    $(btnDestination).prepend($(btnStr));
    $("#removeUserInput").val($(inputVal).val());
  }
});
