$(function () {
  const timeInDropdown = [];
  const timeOutDropdown = [];
  const dailyTotalHours = [];

  for (let i = 0; i < 5; i++) {
    timeInDropdown.push($(`#timeInDropdown_${i}`));
    timeOutDropdown.push($(`#timeOutDropdown_${i}`));
    dailyTotalHours.push($(`#hours_${i}`));

    // set hours when page first loads
    ComputeTotalHours(i);

    timeInDropdown[i].on("input", () => {
      ComputeTotalHours(i);
    });
    timeOutDropdown[i].on("input", () => {
      ComputeTotalHours(i);
    });
  }

  function ComputeTotalHours(index) {
    let dailyTotal = timeOutDropdown[index].val() - timeInDropdown[index].val();
    dailyTotalHours[index].html(dailyTotal);

    let weeklyTotal = 0;
    for (let i = 0; i < 5; i++) {
      if (dailyTotalHours[i]) {
        weeklyTotal += parseFloat(dailyTotalHours[i].html());
      }
    }
    $("#hoursToSendToServer").val(weeklyTotal);
    let outputMessage = "Hours for the week: " + weeklyTotal;

    //error handling
    if (dailyTotal < 0) {
      dailyTotalHours[index].html("ERROR");
      outputMessage = "Time in cannot be after time out.";
      HourErrorHandling(index, "addClass");
    }
    else {
      HourErrorHandling(index, "removeClass");
    }

    $("#weeklyTotalHours").html(outputMessage);
  }

  function HourErrorHandling(index, toggle) {
    timeInDropdown[index][toggle]("err-input");
    timeOutDropdown[index][toggle]("err-input");
  }

  // Populate input values with booleans
  $("#submitBtn").on("click", () => {
    $("#cardSubmittedToSendToServer").val(true);
    PopulateInputValuesWithTimeValues();
  });
  $("#saveBtn").on("click", () => {
    $("#cardSubmittedToSendToServer").val(false);
    PopulateInputValuesWithTimeValues();
  });
  $("#approveBtn").on("click", () => {
    $("#isApprovedToSendToServer").val(true);
    PopulateInputValuesWithTimeValues();
  });
  $("#rejectBtn").on("click", () => {
    $("#isApprovedToSendToServer").val(false);
    PopulateInputValuesWithTimeValues();
  });

  function PopulateInputValuesWithTimeValues() {
    for (let i = 0; i < 5; i++) {
      $(`#timeInToSendToServer_${i}`).val(timeInDropdown[i].val());
      $(`#timeOutToSendToServer_${i}`).val(timeOutDropdown[i].val());
    }
  }
});
