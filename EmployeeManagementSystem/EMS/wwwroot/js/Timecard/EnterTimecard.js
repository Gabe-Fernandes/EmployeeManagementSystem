$(function () {
  HighlightCurrentNavBtn($("#enterTimecardNavBtn"));

  const submitBtn = $("#submitBtn");

  // Adjust your supported times here using 24 hour time
  // This for loop populates the time option dropdowns
  let earliestTime = 7;
  const latestTime = 19;
  const timeInDropdown = [];
  const timeOutDropdown = [];
  const dailyTotalHours = [];

  for (let i = 0; i < 5; i++) {
    timeInDropdown.push($(`#timeInDropdown${i}`));
    timeOutDropdown.push($(`#timeOutDropdown${i}`));
    dailyTotalHours.push($(`#hours${i}`));

    for (let j = earliestTime; j <= latestTime; j += 0.25) {
      timeInDropdown[i].append(GenerateTimeOptions(j));
      timeOutDropdown[i].append(GenerateTimeOptions(j));
    }

    ComputeTotalHours(i); // set hours when page first loads

    timeInDropdown[i].on("input", () => {
      ComputeTotalHours(i);
    });
    timeOutDropdown[i].on("input", () => {
      ComputeTotalHours(i);
    });
  }

  function GenerateTimeOptions(currentTime) {
    const timeOption =
      `<option value="${currentTime}">
      ${ConvertValueToTimeFormat(currentTime)}
      </option>`
    return timeOption;
  }

  function ConvertValueToTimeFormat(timeValue) {
    const meridiemState = (timeValue >= 12) ? "PM" : "AM";
    if (timeValue >= 13) { timeValue -= 12 } // switch to 12 hour time
    timeValue = timeValue.toString(); // allows us to reference chars
    const convertedTime = `${ConvertedHours(timeValue)}:${ConvertedMinutes(timeValue, 2)} ${meridiemState}`;
    return convertedTime;
  }

  function ConvertedHours(timeValue) {
    // if the hours have 2 digits
    if (timeValue.length == 2 || timeValue.charAt(2) == '.') {
      return timeValue.charAt(0) + timeValue.charAt(1);
    }
    else { // if the hours have 1 digit
      return timeValue.charAt(0);
    }
  }

  function ConvertedMinutes(timeValue, index) {
    if (timeValue.length < 3) { return "00" } // x.0

    if (timeValue.charAt(index - 1) == '.') {
      switch (timeValue.charAt(index)) {
        case '2': return "15" // x.25
        case '5': return "30" // x.5
        case '7': return "45" // x.75
      }
    }
    else {
      return ConvertedMinutes(timeValue, 3);
    }
  }



  submitBtn.on("click", () => {
    let btnTxt = (submitBtn.text() == "Submit") ? "Recall" : "Submit";
    submitBtn.text(btnTxt);
  });

  function ComputeTotalHours(index) {
    let dailyTotal = timeOutDropdown[index].val() - timeInDropdown[index].val();
    dailyTotalHours[index].html(dailyTotal);

    let weeklyTotal = 0;
    for (let i = 0; i < 5; i++) {
      if (dailyTotalHours[i]) {
        weeklyTotal += parseFloat(dailyTotalHours[i].html());
      }
    }
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
    $("#weeklyTotalHours")[toggle]("err");
  }
});
