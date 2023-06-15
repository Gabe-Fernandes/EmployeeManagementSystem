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
});
