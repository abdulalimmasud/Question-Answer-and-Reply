<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DealInteface.aspx.cs" Inherits="DealQuestionAnswer.UI.DealInteface" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Content/MainStyleSheet.css" rel="stylesheet" />
    <link href="../Content/DealInterfaceDesign.css" rel="stylesheet" />
    <script src="../Scripts/jquery-3.1.1.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="contanier">
            <header>
                <hgroup id="headerText">
                    <h1>AjkerDeal.com</h1>
                </hgroup>
            </header>
            <div role="main">
                <div id="deal">
                    <p>This is the deal</p>
                    <p>Here is deal detail</p>
                </div>
                <div id="questions">

                    <textarea id="question" name="question"></textarea><br />
                    <button id="btnSaveQuestion" name="save" onclick="SaveQuestion()">Ask Question</button>
                </div>
                <table id="questionAnswer">
                    <thead>
                        <tr>
                            <th colspan="3"><b><u>Questions</u></b></th>
                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
<script>
    $(document).ready(function () {
        GetQuestions();
    });

    function InsertQueDownVote() {
        var id = parseInt($(this).attr('data-key'));
        $.ajax({
            type: 'Post',
            contentType: "application/json; charset=utf-8",
            url: "DealInteface.aspx/InsertQueDownVote",
            data: "{'queId':'" + id + "','userId':'" + 1 + "'}",// 1 should replace by userId which come from session
            async: false,
            success: function (response) {
                alert("Your Vote is accepted");
            }, error: function () {
                alert("Something is error or you are not permitted to vote");
            }
        })
    }
    function InsertQueUpVote() {
        var id = parseInt($(this).attr('data-key'));
        $.ajax({
            type: 'Post',
            contentType: "application/json; charset=utf-8",
            url: "DealInteface.aspx/InsertQueUpVote",
            data: "{'queId':'" + id + "','userId':'" + 1 + "'}",// 1 should replace by userId which come from session
            async: false,
            success: function (response) {
                alert("Thanks for vote");
            }, error: function () {
                alert("Something is error or you are not permitted to vote");
            }
        })
    }
    function SaveQuestion() {
        var que = $('#question').val();
        if (que != '') {
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                url: 'DealInteface.aspx/InsertQuestion',
                data: "{'question':'" + que + "'}",
                async: false,
                success: function (response) {
                    $('#question').val('');
                    alert('Thanks for Question');
                },
                error: function () {
                    alert('Sorry, something is error');
                }
            });
        } else {
            alert('Please, Enter a Question?');
        }
    };
    function GetQuestions() {
        $.ajax({
            type: 'Post',
            contentType: "application/json; charset=utf-8",
            url: "DealInteface.aspx/GetQuestions",
            data: "{'id':'"+1+"'}", //here, 1 will change by deal id
            dataType: "json",
            async: false,
            success: function (result) {
                console.log(result);
                var rows = '';
                for (var i = 0; i < result.d.length; i++) {
                    var question = result[i];
                    rows += '<tr><td><button class="btnQUpVote" data-key=' + result.d[i].Id + '>+</button><br/>';
                    rows += '<label>' + result.d[i].UpVoteCount + '<label><br />'
                    rows += '<button class="btnQDownVote" data-key=' + result.d[i].Id + '>-</button></td>';
                    rows += '<td colspan="2" style="font-size:9px"><strong style="font-size:16px; color:blue">' + result.d[i].Name + '</strong><br />' + result.d[i].DateTime + '</td></tr>';
                    rows += '<tr><td></td><td colspan="2"><div id="que' + result.d[i].Id + '">' + result.d[i].Question + '</div><br />'
                    rows += '<div id="answerDiv' + result.d[i].Id + '"></div><a class="edit" href="javascript:void(0)" data-key=' + result.d[i].Id + '>Edit</a>&nbsp;&nbsp;'
                    rows += '<a class="answer" href="javascript:void(0)" data-key=' + result.d[i].Id + '>Answer</a>&nbsp;';
                    rows += '<a class="delete" href="javascript:void(0)" data-key=' + result.d[i].Id + '>Delete</a>&nbsp';
                    rows += '<a class="seeAnswer" href="javascript:void(0)" data-key=' + result.d[i].Id + '>See Answers</a><br/>';
                    rows += '<table id="tblAnswer' + result.d[i].Id + '"><tbody><tbody></table></td></tr>';
                    //GetAnswer(result.d[i].Id)
                }
                $('#questionAnswer tbody').html(rows);
                $('#questionAnswer button.btnQUpVote').on('click', InsertQueUpVote);
                $('#questionAnswer button.btnQDownVote').on('click', InsertQueDownVote);
                var valueofedit = $('#questionAnswer a.edit').val();
                $('#questionAnswer a.edit').on('click', QuestionEdit);
                $('#questionAnswer a.answer').on('click', AnswerDiv);
                $('#questionAnswer a.seeAnswer').on('click', GetAnswer);
                $('#questionAnswer a.delete').on('click', DeleteQuestion);

            }, error: function (error) {
                console.log(error);
                alert("Something error");
            }
        });
    };
    function QuestionEdit() {
        var key = parseInt($(this).attr('data-key'));
        var divid = '#que' + key;
        var editDiv = '<textarea type="text" id="txtUpdateText' + key + '">' + $(divid).html() + '</textarea><br/>';
        editDiv += '<a class="cancel" href="javascript:void(0)" data-key=' + key + '>Cancel</a> &nbsp;'
        editDiv += '<a id="update' + key + '" href="javascript:void(0)" data-key=' + key + '>Update</a>';
        $(divid).html(editDiv);
        $(this).empty();
        $('#questionAnswer a.answer').empty();
        $('#questionAnswer a.delete[data-key=' + key + ']').fadeOut(0);
        var btnId = '#update' + key;        
        $(btnId).on('click', UpdateQuestion);
        $('#questionAnswer a.cancel').on('click', GetQuestions);
    }
    function UpdateQuestion() {
        var key = parseInt($(this).attr('data-key'));        
        var txtId = "#txtUpdateText" + key;
        var txtValue = $(txtId).val();
        console.log(txtValue);
        $.ajax({
            type: 'Post',
            contentType: 'application/json; charset:utf-8',
            url: 'DealInteface.aspx/UpdateQuestion',
            data: "{'queId':'" + key + "','question':'" + $(txtId).val() + "'}",
            async: false,
            success: function () {
                GetQuestions()
                alert("Your Question is updated");
            }, error: function () {
                alert("Something is error");
            }
        })
    }
    function DeleteQuestion() {
        var id = parseInt($(this).attr('data-key'));
        if (confirm("Are You Sure, You want to delete")) {
            $.ajax({
                type: 'Post',
                contentType: 'application/json; charset=utf-8',
                url: "DealInteface.aspx/DeleteQuestion",
                data: "{'id':'" + id + "'}",
                async: false,
                success: function (response) {
                    alert("Your Question is deleted ")
                }, error: function () {
                    alert("Sorry, You are not permitted to delete Question");
                }
            });
        }        
    }
    function AnswerDiv() {
        var key = parseInt($(this).attr('data-key'));
        var divid = '#answerDiv' + key;
        $('#questionAnswer a.edit[data-key=' + key + ']').fadeOut(0);
        $('#questionAnswer a.answer[data-key=' + key + ']').fadeOut(0);
        $('#questionAnswer a.delete[data-key=' + key + ']').fadeOut(0);
        var editDiv = '<textarea id="txtAnswer' + key + '"></textarea><br/>';
        editDiv += '<a class="cancel" href="javascript:void(0)" data-key=' + key + '>Cancel</a> &emsp;'
        editDiv += '<a class="qAnswer" href="javascript:void(0)" data-key=' + key + '>Answer</a>'

        $(divid).html(editDiv);
        $('#questionAnswer a.qAnswer').on('click', InsertAnswer);
        $('#questionAnswer a.cancel').on('click', GetQuestions);
    }
    function InsertAnswer() {
        var id = parseInt($(this).attr('data-key'));
        var txtId = '#txtAnswer' + id;
        $.ajax({
            type: 'Post',
            contentType: "application/json; charset=utf-8",
            url: "DealInteface.aspx/InsertAnswer",
            data: "{'ans':'" + $(txtId).val() + "','qId':'" + id + "','uId':'" + 1 + "'}",
            success: function (response) {
                alert('Thanks for answer');
            }, error: function () {
                alert('something is error');
            }
        })
    }
    function GetAnswer() {
        var id =parseInt($(this).attr('data-key'));
        var tblId = '#tblAnswer' + id;
        console.log(tblId)
        $.ajax({
            type: 'Post',
            contentType: "application/json; charset=utf-8",
            url: "DealInteface.aspx/GetAnswers",
            data: "{'id':'" + id + "'}",
            async: false,
            success: function (data) {
                var rows = '';
                for (var i = 0; i < data.d.length; i++) {
                    var answer = data.d[i];
                    rows += '<tr><td><button class="btnAnsUpVote" data-key=' + answer.Id + '>+</button><br/>';
                    rows += '<label>' + answer.UpVoteCount + '<label><br/>';
                    rows += '<button class="btnAnsDownVote" data-key=' + answer.Id + '>-</button></td>';
                    rows += '<td colspan="2" style="font-size:9px"><strong style="font-size:14px; color:green">' + answer.Name + '</strong><br />' + data.d[i].DateTime + '</td></tr>'
                    rows += '<tr><td></td><td><div id="ans' + answer.Id + '">' + answer.Answer + '</div><br/><div id="replyDiv'+answer.Id+'"></div>'
                    rows += '<a class="answerEdit" data-key=' + answer.Id + ' href="javascript:void(0)">Edit</a>&nbsp;'
                    rows += '<a class="reply" href="javascript:void(0)" data-key=' + answer.Id + '>Reply</a> &nbsp;'
                    rows += '<a class="deleteAnswer" href="javascript:void(0)" data-key=' + answer.Id + '>Delete</a>&emsp;'
                    rows += '<a class="replys" href="javascript:void(0)" data-key=' + answer.Id + '>See Replys</a>';
                    rows += '<table id="tblReply' + answer.Id + '"><tbody><tbody><table></td></tr>';

                }
                rows = rows || '<tr><td>No Answer Available</td></tr>';
                $(tblId).html(rows);
            }            
        })

        $('.btnAnsUpVote').on('click', InsertAnsUpVote);
        $('.btnAnsDownVote').on('click', InsertAnsDownVote);
        $('.answerEdit').on('click', AnswerEdit);
        $('.deleteAnswer').on('click', DeleteAnswer);
        $('.reply').on('click', ReplyDiv);
        $('.replys').on('click', GetReply);
    }
    function InsertAnsUpVote() {
        var id = parseInt($(this).attr('data-key'));
        $.ajax({
            type: 'Post',
            contentType: 'application/json; charset=utf-8',
            url: "DealInteface.aspx/InsertAnsUpVote",
            data: "{'ansId':'" + id + "','userId':'" + 1 + "'}",// 1 should replace by userId which come from session
            async: false,
            success: function (response) {
                alert('Thanks for voting');
            }, error: function () {
                alert('Something is error');
            }
        })
    }
    function InsertAnsDownVote() {
        var id = parseInt($(this).attr('data-key'));
        $.ajax({
            type: 'Post',
            contentType: 'application/json; charset=utf-8',
            url: "DealInteface.aspx/InsertAnsDownVote",
            data: "{'ansId':'" + id + "','userId':'" + 1 + "'}",// 1 should replace by userId which come from session
            async: false,
            success: function (response) {
                alert('Your vote is accepted');
            }, error: function () {
                alert('Something is error');
            }
        })
    }
    function AnswerEdit() {
        var key = parseInt($(this).attr('data-key'));
        var divId = '#ans' + key;
        var editDiv = '<textarea id="txtUpdateAns' + key + '" style="max-width:180px">' + $(divId).html() + '</textarea><br/>'
        editDiv += '<a class="cancel" href="javascript:void(0)">Cancel</a>&nbsp;'
        editDiv += '<a class="updateAnswer" href="javascript:void(0)" data-key=' + key + '>Update</a>'
        $(divId).html(editDiv);
        $(this).fadeOut(0);
        $('.reply[data-key=' + key + ']').fadeOut(0);
        $('.deleteAnswer[data-key=' + key + ']').fadeOut(0);
        $('.replys[data-key=' + key + ']').fadeOut(0);
        $('.updateAnswer').on('click', UpdateAnswer);
    }
    function UpdateAnswer() {
        var key = parseInt($(this).attr('data-key'));
        var txtId = "#txtUpdateAns" + key;
        var txtValue = $(txtId).val();
        console.log(txtValue);
        $.ajax({
            type: 'Post',
            contentType: 'application/json; charset:utf-8',
            url: 'DealInteface.aspx/UpdateAnswer',
            data: "{'ansId':'" + key + "','answer':'" + $(txtId).val() + "'}",
            async: false,
            success: function () {
                alert("Your Answer is updated");
            }, error: function () {
                alert("Something is error");
            }
        })
    }
    function DeleteAnswer() {
        var id = parseInt($(this).attr('data-key'));
        if (confirm("Are You Sure, You want to delete")) {
            $.ajax({
                type: 'Post',
                contentType: 'application/json; charset=utf-8',
                url: "DealInteface.aspx/DeleteAnswer",
                data: "{'id':'" + id + "'}",
                async: false,
                success: function (response) {
                    alert("Your Answer is deleted ")
                }, error: function () {
                    alert("Sorry, You are not permitted to delete Answer");
                }
            });
        }
    }
    function ReplyDiv() {
        var key = parseInt($(this).attr('data-key'));
        var divid = '#replyDiv' + key;
        console.log(divid);
        $(this).fadeOut(0);
        $('.answerEdit[data-key=' + key + ']').fadeOut(0);
        $('.deleteAnswer[data-key=' + key + ']').fadeOut(0);
        $('.replys[data-key=' + key + ']').fadeOut(0);

        var editDiv = '<textarea id="txtReply' + key + '"></textarea><br/>';
        editDiv += '<a class="cancel" href="javascript:void(0)" data-key=' + key + '>Cancel</a> &emsp;'
        editDiv += '<a class="aReply" href="javascript:void(0)" data-key=' + key + '>Reply</a>';
        $(divid).html(editDiv);
        $('.aReply[data-key='+key+']').on('click');
        $('a.cancel').on('click');
        $('a.aReply').on('click', InsertReply);
    }
    function InsertReply() {
        var id = parseInt($(this).attr('data-key'));
        var txtId = '#txtReply' + id;
        $.ajax({
            type: 'Post',
            contentType: "application/json; charset=utf-8",
            url: "DealInteface.aspx/InsertReply",
            data: "{'rply':'" + $(txtId).val() + "','aId':'" + id + "','uId':'" + 1 + "'}",
            success: function (response) {
                alert('Thanks for Reply');
            }, error: function () {
                alert('something is error');
            }
        })
    }
    function GetReply() {
        var id = parseInt($(this).attr('data-key'));
        var tblId = '#tblReply' + id;
        $.ajax({
            type: 'Post',
            contentType: 'application/json; charset=utf-8',
            url: 'DealInteface.aspx/GetReplys',
            data: "{'id':'" + id + "'}",
            async: false,
            success: function (response) {
                var rows = '';
                for (var i = 0; i < response.d.length; i++) {
                    var reply = response.d[i];
                    rows += '<tr><td style="font-size:9px"><strong style="font-size:13px; color:violet">' + reply.UserName + '</strong><br />' + reply.DateTime + '</td></tr><tr>';
                    rows += '<td><div id="replysDiv'+reply.Id+'">' + reply.Reply + '</div><br/>';
                    rows += '<a class="replyEdit" href="javascript:void(0)" data-key=' + reply.Id + '>Edit</a>&nbsp;';
                    rows += '<a class="deleteReply" href="javascript:void(0)" data-key=' + reply.Id + '>Delete</a>'
                    rows += '</td></tr>';
                }
                $(tblId).html(rows);
            }, error: function () {
                alert('Something is error');
            }
        });
        $('.replyEdit').on('click', ReplyEdit);
        $('.deleteReply').on('click', DeleteReply);
    }
    function ReplyEdit() {
        var key = parseInt($(this).attr('data-key'));
        var divId = '#replysDiv' + key;
        var editDiv = '<textarea class="txtUpdateReply" id="txtUpdateReply' + key + '">' + $(divId).html() + '</textarea><br/>'
        editDiv += '<a class="cancel" href="javascript:void(0)">Cancel</a>&nbsp;'
        editDiv += '<a class="updateReply" href="javascript:void(0)" data-key=' + key + '>Update</a>'
        $(divId).html(editDiv);
        $(this).fadeOut(0);
        $('.deleteReply[data-key=' + key + ']').fadeOut(0);
        $('.updateReply').on('click',UpdateReply)
    }
    function UpdateReply() {
        var key = parseInt($(this).attr('data-key'));
        var txtId = "#txtUpdateReply" + key;
        var txtValue = $(txtId).val();
        console.log(txtValue);
        $.ajax({
            type: 'Post',
            contentType: 'application/json; charset:utf-8',
            url: 'DealInteface.aspx/UpdateReply',
            data: "{'repId':'" + key + "','reply':'" + $(txtId).val() + "'}",
            async: false,
            success: function () {
                alert("Your Reply is updated");
            }, error: function () {
                alert("Something is error");
            }
        })
    }
    function DeleteReply() {
        var id = parseInt($(this).attr('data-key'));
        console.log(id);
        if (confirm("Are You Sure, You want to delete")) {
            $.ajax({
                type: 'Post',
                contentType: 'application/json; charset=utf-8',
                url: "DealInteface.aspx/DeleteReply",
                data: "{'repId':'" + id + "'}",
                async: false,
                success: function (response) {
                    alert("Your Reply is deleted ")
                }, error: function () {
                    alert("Sorry, You are not permitted to delete Reply");
                }
            });
        }
    }
</script>
