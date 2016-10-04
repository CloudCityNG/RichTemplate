<%@ Page Language="C#" AutoEventWireup="true"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="formBlog">
        <div id="message"></div>
        <div>
            List of Blogs<br/>
            <div id="blogList"></div>
        </div>
    <div>
        <h2>Get Blog by Id:</h2>
        <input id="id" name="id" type="text"/>
        <input id="searchBlog" type="button" value="Search by ID"/>
    </div>
    <div>
        <h2>Add Blog</h2>
        title: <input id="title" name="title" type="text"/><br/>
        description: <input id="description" name="description" type="text"/><br/>
        <input id="addBlog" type="submit" value="Add Blog"/>
    </div>
        <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-2.0.3.min.js"></script>
        <script src="../scripts/angular.min.js"></script>
        <script type="text/javascript">
            var blogUri = '/api/blogs';
            $(document).ready(function () {
                getAll();
                $('#searchBlog').click(function() {
                    var id = $('#id').val();
                    if (id.length > 0) {
                        $.getJSON(blogUri + '/' + id)
                            .done(function (data) {
                                $('#title').val(data.Title);
                                $('#description').val(data.Description);
                            })
                            .fail(function(jqXHR, textStatus, err) {
                                $('#message').text('Error: ' + err);
                            });
                    }
                });
                $("#formBlog").submit(function () {
                    var jqxhr = $.post('/api/blogs', $('#formBlog').serialize())
                        .success(function () {
                            getAll();
                        })
                        .error(function () {
                            $('#message').html("Error posting the update.");
                        });
                    return false;
                });
            });
            function formatBlog(item) {
                return "<span id='blogId'>" + item.Id + "</span><br/>" + item.Title + " <input type='button' value='delete' onclick='deleteBlog(this)'/><br/>" + item.Description + "<br/>" + item.DateCreated + "<hr/>";
            }
            function getAll() {
                // send and WEB API ajax request
                $.getJSON(blogUri).done(function (data) {
                    $('#blogList').empty();
                    $('<hr/>').appendTo($('#blogList'));
                    $.each(data, function (key, item) {
                        $('<div>', { html: formatBlog(item) }).appendTo($('#blogList'));
                    });
                });
            }
            function deleteBlog(element) {
                var id = $(element).siblings("#blogId").text();
                if (id.length > 0) {
                    $.ajax({
                        url: blogUri + '/' + id,
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
