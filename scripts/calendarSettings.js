/**
 * Created by princ on 19.10.2016 г..
 */
//Calendar Settings
jQuery(document).ready(function () {

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
        $cal = $('#calendar');

    function daysDifference(firstDate, secondDate) {
        return Math.round((secondDate.getTime() - firstDate.getTime())/(24*60*60*1000)) ;
    }
    var today = new Date();
    var dateAfter = new Date(2016,10,15+1);
    var dateBefore = new Date(2016,11,24);
    var minDate = daysDifference(today, dateAfter);
    today.setHours(0,0,0,0);

    function datesFromJSONArray(json) {
        let array = [];
        $.each(json.Dates, function () {
            array.push(new Date(this.Year, this.Month - 1, this.Day).getTime());
        });
        return array;
    }

    var datesArray = datesFromJSONArray(dateDetails);
    $cal.datepicker(
        {
            inline: true,
            firstDay: 1,
            showOtherMonths: true,
            dayNamesMin: ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'],
            beforeShowDay: function (date) {
                let highlight = $.inArray(date.getTime(), datesArray) >= 0,
                    elIndex = $.inArray(date.getTime(), datesArray);
                if (elIndex >= 0) {
                    percentAttenders = parseInt(dateDetails.Dates[elIndex].FreePeopleCount / allAttenders * 10);
                }
                if (highlight) {
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
                } else {
                    return [true, '', ''];
                }
            },
            minDate: minDate,
            dateFormat: 'dd/mm/yy',
            maxDate: dateBefore
        });
});