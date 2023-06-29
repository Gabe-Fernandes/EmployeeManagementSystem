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

function thSortEvent(tbody, directionFlag, rowNamespace, tdSortClass) {
  const rowCount = $(`#${tbody}`).children().length;

  // sort content
  let sortedRows = [];
  for (let i = 0; i < rowCount; i++) {
    const row = $(`#${rowNamespace}_${i}`)
    const rowText = row.children(`.${tdSortClass}`).html();
    const rowObj = { row: row, rowText: rowText };
    sortedRows = sortRows(sortedRows, rowObj);
  }

  // move content
  const appendDirection = directionFlag ? "append" : "prepend";
  for (let i = 0; i < rowCount; i++) {
    $(`#${tbody}`)[appendDirection](sortedRows[i].row);
  }
  return !directionFlag;
}
function sortRows(sortedRows, rowObj) {
  for (let i = 0; i < sortedRows.length; i++) {
    // if the provided row is alphabetically before the row at index i, insert the rowObj
    if (alphabeticallyFirst(rowObj.rowText, sortedRows[i].rowText) === rowObj.rowText) {
      sortedRows.splice(i, 0, rowObj);
      return sortedRows;
    }
  }
  // if the condition is never met, rowObj comes last
  sortedRows.push(rowObj);
  return sortedRows;
}
function alphabeticallyFirst(string1, string2) {
  for (let i = 0; i < string2.length; i++) {
    if (string1[i] === string2[i]) { continue; }
    if (string1[i] === undefined) { return string1; } // cases where chars match, but one string has more chars
    if (string2[i] === undefined) { return string2; }
    return (string1.toLowerCase().charCodeAt(i) < string2.toLowerCase().charCodeAt(i)) ? string1 : string2;
  }
  // if every char was a match
  return string2;
}

// Move mobile nav btns from navbar to mobile navbar

function moveNavBtns() {
  // if entering mobile mode
  if ($(window).width() <= 768) {
    $("#mobileNavContainer").append($("nav").children(".nav-btn"));
    $("#mobileNavContainer").append($("nav").children(".profile-link"));
    $("#mobileNavContainer").append($("nav").children(".logout-btn"));
  }
  // if exiting mobile mode
  else {
    $("nav").append($("#mobileNavContainer").children());
  }
}

// execute once on each page load
moveNavBtns();

$(window).on("resize", moveNavBtns);

// Mobile Nav Menu Toggle
$("#mobileNavBtn").on("click", () => {
  if ($("#mobileNavContainer").hasClass("slide-mobile-nav")) {
    $("#mobileNavContainer").removeClass("slide-mobile-nav");
  }
  else {
    $("#mobileNavContainer").addClass("slide-mobile-nav");
  }
});
