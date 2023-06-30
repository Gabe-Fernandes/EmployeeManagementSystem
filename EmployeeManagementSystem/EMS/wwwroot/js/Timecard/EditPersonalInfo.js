$(function () {
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
    let errorExists = RunCommonValidationTests(allInputFields, allErrIDs, 40);

    if (ValidEmail($("#editPersonalInfoEmail").val()) === false) {
      ShowError("editPersonalInfoEmail", "editPersonalInfoEmailErr", "invalid email");
      errorExists = true;
    }
    if (DateIsPastDate("editPersonalInfoDOB", "editPersonalInfoDOBErr") === false) {
      errorExists = true;
    }
    if (ValidatePostalCode("editPersonalInfoPostalCode", "editPersonalInfoPostalCodeErr") === false) {
      errorExists = true;
    }
    if (ValidatePhoneNumber("editPersonalInfoPhoneNumber", "editPersonalInfoPhoneNumberErr") === false) {
      errorExists = true;
    }

    if (errorExists) { evt.preventDefault() }
  });

  // Clear Error Handling Events

  for (let i = 0; i < allInputIDs.length; i++) {
    $(`#${allInputIDs[i]}`).on("input", () => {
      HideError(allInputIDs[i], allErrIDs[i]);
    });
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
