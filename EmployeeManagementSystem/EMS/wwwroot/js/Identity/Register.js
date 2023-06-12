$(function () {
  let passValid = false;
  const allInputNames = ["Email", "FirstName", "LastName", "PhoneNumber", "DOB", "StreetAddress",
                         "City", "State", "PostalCode", "Password", "ConfirmPassword"];
  let allInputIDs = [];
  let allInputFields = [];
  let allErrIDs = [];

  for (let i = 0; i < allInputNames.length; i++) {
    allInputIDs.push(`register${allInputNames[i]}`);
    allInputFields.push($(`#${allInputIDs[i]}`));
    allErrIDs.push(`${allInputIDs[i]}Err`);
  }

  // Validation Events

  $("#registerForm").on("submit", (evt) => {
    let errorExists = RunCommonValidationTests(allInputFields, allErrIDs, 40);

    if (ValidEmail($("#registerEmail").val()) === false) {
      ShowError("registerEmail", "registerEmailErr", "invalid email");
      errorExists = true;
    }
    if (CheckPasswordMatch($("#registerPassword").val(), "registerConfirmPassword", "registerConfirmPasswordErr") === false) {
      errorExists = true;
    }
    if (DateIsFutureDate("registerDOB", "registerDOBErr") === false) {
      errorExists = true;
    }
    if (ValidatePostalCode("registerPostalCode", "registerPostalCodeErr") === false) {
      errorExists = true;
    }
    if (ValidatePhoneNumber("registerPhoneNumber", "registerPhoneNumberErr") === false) {
      errorExists = true;
    }

    if (errorExists || !passValid) { evt.preventDefault() }
  });

  $("#registerPassword").on("input", () => {
    passValid = LivePasswordValidation($("#registerPassword").val());
  });

  // Clear Error Handling Events

  for (let i = 0; i < allInputIDs.length; i++) {
    $(`#${allInputIDs[i]}`).on("input", () => {
      HideError(allInputIDs[i], allErrIDs[i]);
    });
  }

  // Misc. Events
  PhoneNumberFormatting($("#registerPhoneNumber"));
});
