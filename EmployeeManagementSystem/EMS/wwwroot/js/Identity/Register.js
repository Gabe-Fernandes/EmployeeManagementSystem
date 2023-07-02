$(function () {
  const charLimit = 40;
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
    RunCommonValidationTests(allInputFields, allErrIDs, charLimit);

    if (ValidEmail($("#registerEmail").val()) === false) {
      ShowError("registerEmail", "registerEmailErr", "invalid email");
    }
    CheckPasswordMatch($("#registerPassword").val(), "registerConfirmPassword", "registerConfirmPasswordErr");
    DateIsPastDate("registerDOB", "registerDOBErr");
    ValidatePostalCode("registerPostalCode", "registerPostalCodeErr");
    ValidatePhoneNumber("registerPhoneNumber", "registerPhoneNumberErr");

    if ($(".err-input").length > 0) { evt.preventDefault() }
  });

  // Real-Time Validation
  realTimeValidation(allInputIDs, allErrIDs, allInputFields, charLimit, registerValidations);

  function registerValidations(i) {
    if (allInputIDs[i] === "registerEmail") {
      if (ValidEmail(allInputFields[i].val()) === false) {
        ShowError(allInputIDs[i], allErrIDs[i], "invalid email");
        return true;
      }
    }
    else if (allInputIDs[i] === "registerPassword") {
      if (LivePasswordValidation(allInputFields[i].val()) === false) {
        return true;
      }
    }
    else if (allInputIDs[i] === "registerConfirmPassword") {
      if (CheckPasswordMatch($("#registerPassword").val(), allInputIDs[i], allErrIDs[i]) === false) {
        return true;
      }
    }
    else if (allInputIDs[i] === "registerDOB") {
      if (DateIsPastDate(allInputIDs[i], allErrIDs[i]) === false) {
        return true;
      }
    }
    else if (allInputIDs[i] === "registerPostalCode") {
      if (ValidatePostalCode(allInputIDs[i], allErrIDs[i]) === false) {
        return true;
      }
    }
    else if (allInputIDs[i] === "registerPhoneNumber" && allInputFields[i].val().length <= 12) {
      if (ValidatePhoneNumber(allInputIDs[i], allErrIDs[i]) === false) {
        return true;
      }
    }
  }

  // Phone Formatting

  PhoneNumberFormatting($("#registerPhoneNumber"));

  // Pass Show Toggle

  $("#hidePassBtn").on("click", () => {
    TogglePasswordShow($("#registerPassword"), $("#hidePassBtn"), $("#showPassBtn"));
    TogglePasswordShow($("#registerConfirmPassword"), $("#hideRepeatPassBtn"), $("#showRepeatPassBtn"));
  });
  $("#showPassBtn").on("click", () => {
    TogglePasswordShow($("#registerPassword"), $("#showPassBtn"), $("#hidePassBtn"));
    TogglePasswordShow($("#registerConfirmPassword"), $("#showRepeatPassBtn"), $("#hideRepeatPassBtn"));
  });
  $("#hideRepeatPassBtn").on("click", () => {
    TogglePasswordShow($("#registerPassword"), $("#hidePassBtn"), $("#showPassBtn"));
    TogglePasswordShow($("#registerConfirmPassword"), $("#hideRepeatPassBtn"), $("#showRepeatPassBtn"));
  });
  $("#showRepeatPassBtn").on("click", () => {
    TogglePasswordShow($("#registerPassword"), $("#showPassBtn"), $("#hidePassBtn"));
    TogglePasswordShow($("#registerConfirmPassword"), $("#showRepeatPassBtn"), $("#hideRepeatPassBtn"));
  });

  // Hide 2nd and 3rd pages when switching to paginated mobile mode

  function hidePaginatedContent() {
    // if entering mobile mode
    if ($(window).width() <= 768) {
      if (!$("#mobilePage2").hasClass("hide") && !$("#mobilePage3").hasClass("hide")) {
        $("#mobilePage2").addClass("hide");
        $("#mobilePage3").addClass("hide");
      }
    }
    // if exiting mobile mode
    else {
      $("#mobilePage1").removeClass("hide");
      $("#mobilePage2").removeClass("hide");
      $("#mobilePage3").removeClass("hide");
    }
  }
  // execute once on each page load
  hidePaginatedContent();
  $(window).on("resize", hidePaginatedContent);

  // Mobile Pagination

  $("#mobilePaginationRight1").on("click", () => {
    $("#mobilePage1").addClass("hide");
    $("#mobilePage2").removeClass("hide");
    $("#mobilePage3").addClass("hide");
  });
  $("#mobilePaginationLeft2").on("click", () => {
    $("#mobilePage1").removeClass("hide");
    $("#mobilePage2").addClass("hide");
    $("#mobilePage3").addClass("hide");
  });
  $("#mobilePaginationRight2").on("click", () => {
    $("#mobilePage1").addClass("hide");
    $("#mobilePage2").addClass("hide");
    $("#mobilePage3").removeClass("hide");
  });
  $("#mobilePaginationLeft3").on("click", () => {
    $("#mobilePage1").addClass("hide");
    $("#mobilePage2").removeClass("hide");
    $("#mobilePage3").addClass("hide");
  });
});
