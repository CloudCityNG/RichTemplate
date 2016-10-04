<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="formEvent">
        <div id="message"></div>
        <div>
            List of Events<br/>
            <div id="eventList"></div>
        </div>
    <div>
        <h2>Get Event by Id:</h2>
        <input id="id" name="id" type="text"/>
        <input id="searchEvent" type="button" value="Search by ID"/>
    </div>
    <div>
        <h2>Add Event</h2>
        title: <input id="title" name="title" type="text"/><br/>
        description: <input id="description" name="description" type="text"/><br/>
        <input id="addEvent" type="submit" value="Add Event"/>
    </div>
        <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-2.0.3.min.js"></script>
        <script src="../scripts/angular.min.js"></script>
        <script type="text/javascript">
            var eventUri = '/api/events';
            $(document).ready(function () {
                getAll();
                $('#searchEvent').click(function() {
                    var id = $('#id').val();
                    if (id.length > 0) {
                        $.getJSON(eventUri + '/' + id)
                            .done(function (data) {
                                $('#title').val(data.Title);
                                $('#description').val(data.Description);
                            })
                            .fail(function(jqXHR, textStatus, err) {
                                $('#message').text('Error: ' + err);
                            });
                    }
                });
                $("#formEvent").submit(function () {
                    var jqxhr = $.post('/api/events', $('#formEvent').serialize())
                        .success(function () {
                            getAll();
                        })
                        .error(function () {
                            $('#message').html("Error posting the update.");
                        });
                    return false;
                });
            });
            function formatEvent(item) {
                return "<span id='eventId'>" + item.Id + "</span><br/>" + item.Title + " <input type='button' value='delete' onclick='deleteEvent(this)'/><br/>" + item.Description + "<br/>" + item.DateCreated + "<hr/>";
            }
            function getAll() {
                // send and WEB API ajax request
                $.getJSON(eventUri).done(function (data) {
                    $('#eventList').empty();
                    $('<hr/>').appendTo($('#eventList'));
                    $.each(data, function (key, item) {
                        $('<div>', { html: formatEvent(item) }).appendTo($('#eventList'));
                    });
                });
            }
            function deleteEvent(element) {
                var id = $(element).siblings("#eventId").text();
                if (id.length > 0) {
                    $.ajax({
                        url: eventUri + '/' + id,
                        type: 'DELETE',
                        success: function(result) {
                            getAll();
                        },
                        error: function(error) {
                            $('#message').text('Error: ' + error);
                        }
                    });
                }
            }
        </script>
    </form>
</body>
</html>
