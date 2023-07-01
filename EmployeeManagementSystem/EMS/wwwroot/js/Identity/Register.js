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
    if (DateIsPastDate("registerDOB", "registerDOBErr") === false) {
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
