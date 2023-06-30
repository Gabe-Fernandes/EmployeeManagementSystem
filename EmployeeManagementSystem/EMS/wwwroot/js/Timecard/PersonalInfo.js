$(function () {
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
  $("#mobilePage1").removeClass("hide");
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
