$(function () {
  // Validation Events

  $("#loginForm").on("submit", (evt) => {
    const inputFields = [$("#emailLoginInput"), $("#passwordLoginInput")];
    const errIDs = ["errEmailLoginInput", "errPasswordLoginInput"];

    let errorExists = RunCommonValidationTests(inputFields, errIDs, 40);

    // test valid email
    if (ValidEmail($("#emailLoginInput").val()) === false) {
      ShowError("emailLoginInput", "errEmailLoginInput", "invalid email");
      errorExists = true;
    }

    if (errorExists) { evt.preventDefault() }
  });
  
  $("#recoverPassForm").on("submit", (evt) => {
    const inputFields = [$("#forgotPassEmail")];
    const errIDs = ["errforgotPassEmail"];

    let errorExists = RunCommonValidationTests(inputFields, errIDs, 40);

    // test valid email
    if (ValidEmail($("#forgotPassEmail").val()) === false) {
      ShowError("forgotPassEmail", "errforgotPassEmail", "invalid email");
      errorExists = true;
    }

    if (errorExists) { evt.preventDefault() }
  });

  $("#resendEmailConfForm").on("submit", (evt) => {
    const inputFields = [$("#resendEmailConfEmail")];
    const errIDs = ["errResendEmailConfEmail"];

    let errorExists = RunCommonValidationTests(inputFields, errIDs, 40);

    // test valid email
    if (ValidEmail($("#resendEmailConfEmail").val()) === false) {
      ShowError("resendEmailConfEmail", "errResendEmailConfEmail", "invalid email");
      errorExists = true;
    }

    if (errorExists) { evt.preventDefault() }
  });

  // Clear Error Handling Events

  const allInputIDs = ["emailLoginInput", "passwordLoginInput", "forgotPassEmail", "resendEmailConfEmail"];
  const allErrIDs = ["errEmailLoginInput", "errPasswordLoginInput", "errforgotPassEmail", "errResendEmailConfEmail"];

  for (let i = 0; i < allInputIDs.length; i++) {
    $(`#${allInputIDs[i]}`).on("input", () => {
      HideError(allInputIDs[i], allErrIDs[i]);
    });
  }

  // Modal events

  $("#demoBtn").on("click", () => {
    ToggleModal($("#loginMain"), $("#demoModal"), openModal);
  });
  $("#demoCloseBtn").on("click", () => {
    ToggleModal($("#loginMain"), $("#demoModal"), closeModal);
  });
  $("#forgotPassBtn").on("click", () => {
    ToggleModal($("#loginMain"), $("#forgotPassModal"), openModal);
  });
  $("#forgotPassCloseBtn").on("click", () => {
    ToggleModal($("#loginMain"), $("#forgotPassModal"), closeModal);
  });
  $("#resendEmailConfBtn").on("click", () => {
    ToggleModal($("#loginMain"), $("#resendEmailConfModal"), openModal);
  });
  $("#resendEmailConfCloseBtn").on("click", () => {
    ToggleModal($("#loginMain"), $("#resendEmailConfModal"), closeModal);
  });
});
