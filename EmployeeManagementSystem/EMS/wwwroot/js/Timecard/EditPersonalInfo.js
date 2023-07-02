$(function () {
  const charLimit = 40;
  const allInputNames = ["Email", "FirstName", "LastName", "PhoneNumber", "DOB", "StreetAddress",
    "City", "State", "PostalCode"];
  let allInputIDs = [];
  let allInputFields = [];
  let allErrIDs = [];

  for (let i = 0; i < allInputNames.length; i++) {
    allInputIDs.push(`editPersonalInfo${allInputNames[i]}`);
    allInputFields.push($(`#${allInputIDs[i]}`));
    allErrIDs.push(`${allInputIDs[i]}Err`);
  }

  // Validation Events

  $("#editPersonalInfoForm").on("submit", (evt) => {
    RunCommonValidationTests(allInputFields, allErrIDs, charLimit);
    if (ValidEmail($("#editPersonalInfoEmail").val()) === false) {
      ShowError("editPersonalInfoEmail", "editPersonalInfoEmailErr", "invalid email");
    }
    DateIsPastDate("editPersonalInfoDOB", "editPersonalInfoDOBErr");
    ValidatePostalCode("editPersonalInfoPostalCode", "editPersonalInfoPostalCodeErr");
    ValidatePhoneNumber("editPersonalInfoPhoneNumber", "editPersonalInfoPhoneNumberErr");

    if ($(".err-input").length > 0) { evt.preventDefault() }
  });

  // Real-Time Validation
  realTimeValidation(allInputIDs, allErrIDs, allInputFields, charLimit, editPersonalInfoValidations);

  function editPersonalInfoValidations(i) {
    if (allInputIDs[i] === "editPersonalInfoEmail") {
      if (ValidEmail(allInputFields[i].val()) === false) {
        ShowError(allInputIDs[i], allErrIDs[i], "invalid email");
        return true;
      }
    }
    else if (allInputIDs[i] === "editPersonalInfoDOB") {
      if (DateIsPastDate(allInputIDs[i], allErrIDs[i]) === false) {
        return true;
      }
    }
    else if (allInputIDs[i] === "editPersonalInfoPostalCode") {
      if (ValidatePostalCode(allInputIDs[i], allErrIDs[i]) === false) {
        return true;
      }
    }
    else if (allInputIDs[i] === "editPersonalInfoPhoneNumber" && allInputFields[i].val().length <= 12) {
      if (ValidatePhoneNumber(allInputIDs[i], allErrIDs[i]) === false) {
        return true;
      }
    }
  }

  // Misc. Events
  PhoneNumberFormatting($("#editPersonalInfoPhoneNumber"));

  // Initial phone formatting when the normalized number is loaded from the database
  let phoneNumber = $("#editPersonalInfoPhoneNumber").val();
  if (phoneNumber.includes("-") === false) {
    phoneNumber = phoneNumber.slice(0, 3) + "-" + phoneNumber.slice(3, 6) + "-" + phoneNumber.slice(6, 10);
    $("#editPersonalInfoPhoneNumber").val(phoneNumber);
  }

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
  $("#mobilePage1").removeClass("hide");
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
