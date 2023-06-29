$(function () {
  // Mobile Pagination

  $("#mobilePaginationRight1").on("click", () => {
    $("#mobilePage2").css("left", "0vw");
  });
  $("#mobilePaginationLeft2").on("click", () => {
    $("#mobilePage2").css("left", "100vw");
  });
  $("#mobilePaginationRight2").on("click", () => {
    $("#mobilePage3").css("left", "0vw");
  });
  $("#mobilePaginationLeft3").on("click", () => {
    $("#mobilePage3").css("left", "100vw");
  });
});
