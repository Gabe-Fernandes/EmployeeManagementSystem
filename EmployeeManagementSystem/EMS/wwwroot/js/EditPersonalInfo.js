$(document).ready(function () {
  const firstName = $("#firstName");
  const lastName = $("#lastName");
  const dob = $("#dob");
  const streetAddress = $("#streetAddress");
  const city = $("#city");
  const state = $("#state");
  const zipCode = $("#zipCode");
  const phoneNumber = $("#phoneNumber");
  const email = $("#email");

  const inputFields = [firstName, lastName, dob, streetAddress, city, state, zipCode, phoneNumber, email];

  const addClass = "addClass";
  const removeClass = "removeClass";

  function ValidateRequiredInput() {
    let inputValidity = true;
    for (let i = 0; i < inputFields.length; i++) {
      if (inputFields[i].val() === "") {
        inputValidity = false;
        $("#validationLabel").removeClass("hide");
        ToggleError(inputFields[i], addClass);
      }
    }
    return inputValidity;
  }

  function ToggleError(element, toggle) {
    element[toggle]("err-input");
  }

  for (let i = 0; i < inputFields.length; i++) {
    inputFields[i].on("input", () => {
      ToggleError(inputFields[i], removeClass);
      $("#validationLabel").addClass("hide");
    });
  }

  $("#saveBtn").on("click", (evt) => {
    if (ValidateRequiredInput() === false) {
      evt.preventDefault();
    }
  });
});
