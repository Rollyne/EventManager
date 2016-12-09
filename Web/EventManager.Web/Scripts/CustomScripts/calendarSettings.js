/**
 * Created by princ on 19.10.2016 г..
 */
//Calendar Settings
jQuery(document).ready(function () {
    $('td').addClass('dateCell');
    var dateDetails = (function () {
            let json = null;
            $.ajax({
                'async': false,
                'global': false,
                'url': pathToJson,
                'dataType': "json",
                'success': function (data) {
                    json = data;
                }
            });
            return json;
        }()),
        allAttenders = dateDetails.MaxPeople,
        $cal = $('#calendar'),
        dateAfter = new Date(dateDetails.DateAfter.Year,dateDetails.DateAfter.Month-1,dateDetails.DateAfter.Day),
        dateBefore = new Date(dateDetails.DateBefore.Year,dateDetails.DateBefore.Month-1,dateDetails.DateBefore.Day),
        eventLength = dateDetails.EventLength,
        dateSelectTriggered = false;


    $('.dateSetToggler').on('click', function() {
        if(!dateSelectTriggered) {
            $(this).addClass('selectToggActive');
            $(this).text('Cancel');
            $('#datesSubmit').css({'display': 'block','opacity': '1'});
            $('.ui-datepicker').css('background-color', 'rgba(0,0,0,0.9)');
            dateSelectTriggered = true;
        } else {
            $(this).removeClass('selectToggActive');
            $(this).text('Set Dates');
            $('.ui-datepicker').css('background-color', 'rgba(0,0,0,0.8)');
            $('#datesSubmit').css('opacity', '0');
            setTimeout(function() {
                $('#datesSubmit').hide()
            }, 500);
            $('td').removeClass('calendarHighlightSelected');
            dateSelectTriggered = false;
            selectedDates = [];
            $cal.datepicker("option","beforeShowDay", beforeShow)

        }
    });

    function datesFromJSONArray(json) {
        let array = [];
        $.each(json.Dates, function () {
            array.push(new Date(this.Year, this.Month - 1, this.Day).getTime());
        });
        return array;
    }
    var datesArray = datesFromJSONArray(dateDetails),
        selectedDates = [];
    function beforeShow(date) {
        let highlight = $.inArray(date.getTime(), datesArray) >= 0,
            elIndex = $.inArray(date.getTime(), datesArray),
            selected = $.inArray(date.getTime(), selectedDates) >= 0;
        if (elIndex >= 0) {
            percentAttenders = parseInt(dateDetails.Dates[elIndex].FreePeopleCount / allAttenders * 10);
        }
        if(selected) {
            return [true, 'calendarHighlightSelected', ''];
        } else if (highlight) {
            switch (percentAttenders) {
                case 1 :
                    return [true, "calendarHighlight1", ''];
                    break;
                case 2 :
                    return [true, "calendarHighlight2", ''];
                    break;
                case 3 :
                    return [true, "calendarHighlight3", ''];
                    break;
                case 4 :
                    return [true, "calendarHighlight4", ''];
                    break;
                case 5 :
                    return [true, "calendarHighlight5", ''];
                    break;
                case 6 :
                    return [true, "calendarHighlight6", ''];
                    break;
                case 7 :
                    return [true, "calendarHighlight7", ''];
                    break;
                case 8 :
                    return [true, "calendarHighlight8", ''];
                    break;
                case 9 :
                    return [true, "calendarHighlight9", ''];
                    break;
                case 10 :
                    return [true, "calendarHighlight10", ''];
                    break;
                default :
                    return [true, 'calendarHighlight0', ''];
                    break;
            }
        } else{
            return [true, '', ''];
        }
    }
    $cal.datepicker(
        {
            inline: true,
            firstDay: 1,
            showOtherMonths: true,
            dayNamesMin: ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'],
            dateFormat: 'dd/mm/yy',
            minDate: dateAfter,
            maxDate: dateBefore,
            onSelect: function (date) {
                if (dateSelectTriggered) {
                    selectedDates = [];
                    let dateArr = date.split('/'),
                        startDate = new Date(dateArr[2], dateArr[1] - 1, dateArr[0]),
                        pushedDate = startDate;
                    $('#dateStart').val(date);
                    $('#datesSubmit').prop('disabled', false);
                    for (let i = 0; i < eventLength; i++) {
                        pushedDate = startDate.getTime() + (1000 * 60 * 60 * 24) * i;
                        if (pushedDate > dateBefore.getTime()) {
                            $('#datesSubmit').prop('disabled', true);
                            errorMessages.inputErrorMessage('#calendar', 'There should be ' + eventLength + ' days avaliable');
                            selectedDates = [];
                            $('#dateStart').val('');
                            break;
                        }
                        errorMessages.removeErrorMessage('#calendar');
                        selectedDates.push(pushedDate);
                    }
                }
            }
        });
    $cal.datepicker("option","beforeShowDay", beforeShow);
});