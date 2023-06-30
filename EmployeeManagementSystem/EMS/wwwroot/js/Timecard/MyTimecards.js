$(function () {
  HighlightCurrentNavBtn($("#myTimecardsNavBtn"));

  // TH sorting events

  let thStatusInOrder = true;
  let thDateRangeInOrder = true;
  let thHorusInOrder = true;

  $("#thStatus").on("click", () => {
    thStatusInOrder = thSortEvent("myTimecardsTbody", thStatusInOrder, "myTimecardsTR", "sortStatus", alphabeticallyFirst);
  });
  $("#thDateRange").on("click", () => {
    thDateRangeInOrder = thSortEvent("myTimecardsTbody", thDateRangeInOrder, "myTimecardsTR", "sortDateRange", chronologicallyFirst);
  });
  $("#thHours").on("click", () => {
    thHorusInOrder = thSortEvent("myTimecardsTbody", thHorusInOrder, "myTimecardsTR", "sortHours", numericallyFirst);
  });
});
