const openModal = "O";
const closeModal = "C";

function ToggleModal(main, modal, direction) {
  if (direction === openModal) {
    main.addClass("unclickable");
    modal.removeClass("hide");
  }
  else if (direction === closeModal) {
    main.removeClass("unclickable");
    modal.addClass("hide");
  }
}

function TogglePasswordShow(passEle, eleToHide, eleToShow) {
  eleToHide.addClass("hide");
  eleToShow.removeClass("hide");
  if (passEle.attr("type") === "password") {
    passEle.attr("type", "text");
  }
  else {
    passEle.attr("type", "password");
  }
}

function HighlightCurrentNavBtn(btnToHighlight) {
  $(".nav-btn").removeClass("nav-highlight");
  btnToHighlight.addClass("nav-highlight");
}
