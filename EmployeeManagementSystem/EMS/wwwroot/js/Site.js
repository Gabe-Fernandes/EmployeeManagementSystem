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

function HighlightCurrentNavBtn(btnToHighlight) {
  $(".nav-btn").removeClass("nav-highlight");
  btnToHighlight.addClass("nav-highlight");
}
