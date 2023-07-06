$(function () {
  // Validation Events

  const charLimit = 40;
  const allInputNames = ["Email", "Password", "ForgotPassEmail", "ResendEmailConfEmail"];
  let allInputIDs = [];
  let allInputFields = [];
  let allErrIDs = [];

  for (let i = 0; i < allInputNames.length; i++) {
    allInputIDs.push(`login${allInputNames[i]}`);
    allInputFields.push($(`#${allInputIDs[i]}`));
    allErrIDs.push(`${allInputIDs[i]}Err`);
  }

  $("#loginForm").on("submit", (evt) => {
    const inputFields = [$("#loginEmail"), $("#loginPassword")];
    const errIDs = ["loginEmailErr", "loginPasswordErr"];

    RunCommonValidationTests(inputFields, errIDs, charLimit);

    // test valid email
    if (ValidEmail($("#loginEmail").val()) === false) {
      ShowError("loginEmail", "loginEmailErr", "invalid email");
    }

    if ($(".err-input").length > 0) { evt.preventDefault() }
  });
  
  $("#recoverPassForm").on("submit", (evt) => {
    const inputFields = $("#loginForgotPassEmail");
    const errIDs = "loginForgotPassEmailErr";

    RunCommonValidationTests(inputFields, errIDs, charLimit);

    // test valid email
    if (ValidEmail($("#loginForgotPassEmail").val()) === false) {
      ShowError("loginForgotPassEmail", "loginForgotPassEmailErr", "invalid email");
    }

    if ($(".err-input").length > 0) { evt.preventDefault() }
  });

  $("#resendEmailConfForm").on("submit", (evt) => {
    const inputFields = $("#loginResendEmailConfEmail");
    const errIDs = "loginResendEmailConfEmailErr";

    RunCommonValidationTests(inputFields, errIDs, charLimit);

    // test valid email
    if (ValidEmail($("#loginResendEmailConfEmail").val()) === false) {
      ShowError("loginResendEmailConfEmail", "loginResendEmailConfEmailErr", "invalid email");
    }

    if ($(".err-input").length > 0) { evt.preventDefault() }
  });

  // Real-Time Validation
  realTimeValidation(allInputIDs, allErrIDs, allInputFields, charLimit, loginValidations);

  function loginValidations(i) {
    if (allInputIDs[i] === "loginEmail" || allInputIDs[i] === "loginForgotPassEmail" || allInputIDs[i] === "loginResendEmailConfEmail") {
      if (ValidEmail(allInputFields[i].val()) === false) {
        ShowError(allInputIDs[i], allErrIDs[i], "invalid email");
        return true; // errorExists === true
      }
    }
  }

  // check for failed login on page load
  const invalidCredentialErrMsg = "Email or password are incorrect";
  if ($(".failed-login").length == 1) {
    ShowError("loginEmail", "loginEmailErr", invalidCredentialErrMsg);
    ShowError("loginPassword", "loginPasswordErr", invalidCredentialErrMsg);
  }

  $("#loginEmail").on("input", clearLoginErrosAfterLoginFail);
  $("#loginPassword").on("input", clearLoginErrosAfterLoginFail);
  function clearLoginErrosAfterLoginFail() {
    if ($("#loginPasswordErr")[0].innerHTML === invalidCredentialErrMsg) {
      HideError("loginEmail", "loginEmailErr");
      HideError("loginPassword", "loginPasswordErr");
    }
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

  // open modal if showing confirmation onpageload
  if ($("#recoverPassForm").length === 0) {
    ToggleModal($("#loginMain"), $("#forgotPassModal"), openModal);
  }
  if ($("#resendEmailConfForm").length === 0) {
    ToggleModal($("#loginMain"), $("#resendEmailConfModal"), openModal);
  }

  // Pass Show Toggle

  $("#hidePassBtn").on("click", () => {
    TogglePasswordShow($("#loginPassword"), $("#hidePassBtn"), $("#showPassBtn"));
  });
  $("#showPassBtn").on("click", () => {
    TogglePasswordShow($("#loginPassword"), $("#showPassBtn"), $("#hidePassBtn"));
  });
});
