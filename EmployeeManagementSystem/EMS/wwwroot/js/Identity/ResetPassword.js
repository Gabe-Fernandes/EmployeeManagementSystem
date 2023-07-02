$(function () {
  const charLimit = 40;
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
    RunCommonValidationTests(allInputFields, allErrIDs, charLimit);

    if (ValidEmail($("#resetPassEmail").val()) === false) {
      ShowError("resetPassEmail", "resetPassEmailErr", "invalid email");
    }
    CheckPasswordMatch($("#resetPassPassword").val(), "resetPassConfirmPassword", "resetPassConfirmPasswordErr");

    if ($(".err-input").length > 0) { evt.preventDefault() }
  });

  // Real-Time Validation
  realTimeValidation(allInputIDs, allErrIDs, allInputFields, charLimit, resetPassValidations);

  function resetPassValidations(i) {
    if (allInputIDs[i] === "resetPassEmail") {
      if (ValidEmail(allInputFields[i].val()) === false) {
        ShowError(allInputIDs[i], allErrIDs[i], "invalid email");
        return true;
      }
    }
    else if (allInputIDs[i] === "resetPassPassword") {
      if (LivePasswordValidation(allInputFields[i].val()) === false) {
        return true;
      }
    }
    else if (allInputIDs[i] === "resetPassConfirmPassword") {
      if (CheckPasswordMatch($("#resetPassPassword").val(), allInputIDs[i], allErrIDs[i]) === false) {
        return true;
      }
    }
  }

  // Pass Show Toggle

  $("#hidePassBtn").on("click", () => {
    TogglePasswordShow($("#resetPassPassword"), $("#hidePassBtn"), $("#showPassBtn"));
    TogglePasswordShow($("#resetPassConfirmPassword"), $("#hideRepeatPassBtn"), $("#showRepeatPassBtn"));
  });
  $("#showPassBtn").on("click", () => {
    TogglePasswordShow($("#resetPassPassword"), $("#showPassBtn"), $("#hidePassBtn"));
    TogglePasswordShow($("#resetPassConfirmPassword"), $("#showRepeatPassBtn"), $("#hideRepeatPassBtn"));
  });
  $("#hideRepeatPassBtn").on("click", () => {
    TogglePasswordShow($("#resetPassPassword"), $("#hidePassBtn"), $("#showPassBtn"));
    TogglePasswordShow($("#resetPassConfirmPassword"), $("#hideRepeatPassBtn"), $("#showRepeatPassBtn"));
  });
  $("#showRepeatPassBtn").on("click", () => {
    TogglePasswordShow($("#resetPassPassword"), $("#showPassBtn"), $("#hidePassBtn"));
    TogglePasswordShow($("#resetPassConfirmPassword"), $("#showRepeatPassBtn"), $("#hideRepeatPassBtn"));
  });
});
