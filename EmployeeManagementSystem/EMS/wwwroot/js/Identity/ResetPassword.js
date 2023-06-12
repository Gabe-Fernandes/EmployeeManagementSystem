$(function () {
  let passValid = false;
  const allInputNames = ["Email", "Password", "ConfirmPassword"];
  let allInputIDs = [];
  let allInputFields = [];
  let allErrIDs = [];

  for (let i = 0; i < allInputNames.length; i++) {
    allInputIDs.push(`resetPass${allInputNames[i]}`);
    allInputFields.push($(`#${allInputIDs[i]}`));
    allErrIDs.push(`${allInputIDs[i]}Err`);
  }

  // Validation Events

  $("#resetPasswordForm").on("submit", (evt) => {
    let errorExists = RunCommonValidationTests(allInputFields, allErrIDs, 40);

    if (ValidEmail($("#resetPassEmail").val()) === false) {
      ShowError("resetPassEmail", "resetPassEmailErr", "invalid email");
      errorExists = true;
    }
    if (CheckPasswordMatch($("#resetPassPassword").val(), "resetPassConfirmPassword", "resetPassConfirmPasswordErr") === false) {
      errorExists = true;
    }

    if (errorExists || !passValid) { evt.preventDefault() }
  });

  $("#resetPassPassword").on("input", () => {
    passValid = LivePasswordValidation($("#resetPassPassword").val());
  });

  // Clear Error Handling Events

  for (let i = 0; i < allInputIDs.length; i++) {
    $(`#${allInputIDs[i]}`).on("input", () => {
      HideError(allInputIDs[i], allErrIDs[i]);
    });
  }
});
