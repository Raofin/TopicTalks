let selectedId = null;

function appendInput(questionId, answerId, edit) {
    if (selectedId != answerId) {
        selectedId = answerId;
        closeReplyInput();
    }

    if (!$("#reply-input").length) {

        $(`[id*='answer-${answerId}']`).append(`
                    <div id="reply-input">
                        <form id="reply-form" class="col-sm-9 w-100 d-flex">
                            <input type="hidden" name="answerId" value="${answerId}" />
                            <input type="hidden" name="questionId" value="${questionId}" />
                            <input type="hidden" name="parentAnswerId" value="${answerId}" />
                            <div class="text-center w-100">
                                    <textarea id="reply-text" class="form-control mb-1" rows="3" placeholder="Write your reply here" name="explanation"></textarea>
                            </div>
                            <div class="text-center">
                                <button type="button" class="btn btn-danger w-75 mb-2 mt-1" onclick="closeReplyInput()">Close</button>
                                <button type="submit" class="btn btn-primary w-75">Post</button>
                            </div>
                        </form>
                    </div>`);

        if (edit) {
            $.get(`/answer/${answerId}`)
                .done(function (data) { $("#reply-text").val(data.explanation) })
                .fail(function (error) { toastMessage("Error fetching answer.", ToastType.Error) });
        }

        $('#reply-form').validate({
            rules: {
                explanation: {
                    required: true,
                    minlength: 10
                }
            },
            messages: {
                explanation: {
                    required: "Please enter your explanation",
                    minlength: "Explanation must be at least 10 characters"
                }
            },
            submitHandler: function (form) {
                if (edit) {
                    $.ajax({
                        url: '/answer',
                        type: 'PATCH',
                        data: $(form).serialize(),
                        success: function (data) {
                        console.log(formToJson(form));
                            $("#explanation-" + data.answerId).text(data.explanation);
                            toastMessage("Edit Successful!", ToastType.Success);
                            closeReplyInput();
                        },
                        error: function (xhr) {
                        console.log(formToJson(form));
                            toastMessage(xhr.responseText);
                        }
                    });

                    return;
                }

                $.ajax({
                    url: '/answer',
                    type: 'POST',
                    data: $(form).serialize(),
                    success: function (response) {
                        appendReply(response);
                        toastMessage("Reply Submitted!", ToastType.Success);
                        closeReplyInput();
                    },
                    error: function (xhr) {
                        toastMessage(xhr.responseText);
                    }
                });
            }
        });
    } else {
        closeReplyInput();
    }
}

function fetchAnswer(answerId) {
    $.ajax({
        url: '/answer/' + answerId,
        type: 'GET',
        success: function (data) {
            console.log(data.explanation);
            $("#answer-23 textarea#reply-text").val(function (index, value) {
                return value + explanation;
            });
        },
        error: function (error) {
            toastMessage("Error fetching answer.", ToastType.Error)
        }
    });
}

$('#answer-form').validate({
    rules: {
        explanation: {
            required: true,
            minlength: 10
        }
    },
    messages: {
        explanation: {
            required: "Please enter your explanation",
            minlength: "Explanation must be at least 10 characters"
        }
    },
    submitHandler: function (form) {
        $.ajax({
            url: '/answer',
            type: 'POST',
            data: $(form).serialize(),
            success: function (response) {
                appendAnswer(response);
                toastMessage("Answer Submitted!", ToastType.Success);
                $("#answer-form")[0].reset();
            },
            error: function (xhr) {
                toastMessage(xhr.responseText);
            }
        });
    }
})

function closeReplyInput() {
    selectedId = null;
    $("#reply-input").remove();
}

function appendAnswer(response) {
    console.log(response);
    var answer = `
                <div id="answer-${response.answerId} parent-${response.parentAnswerId}" class="rounded border p-3 mb-3">
                    <div class="d-flex justify-content-between">
                        <p><strong>${response.userInfo.email}</strong> on ${getFormattedDate()} (0 seconds ago)</p>
                        <div class="d-flex gap-2">
                            <span class="link-text text-danger" onclick="deleteAnswer(${response.answerId})">Delete</span>
                            <span class="link-text text-success" onclick="appendInput(${response.questionId}, ${response.answerId}, "edit")">Edit</span>
                            <span class="link-text" onclick="appendInput(${response.questionId}, ${response.answerId})">Reply</span>
                        </div>
                    </div>
                    <div class="form-group row">
                        <p>${response.explanation}</p>
                    </div>
                </div>`;

    $("#answer-container").append(answer);
}

function appendReply(response) {
    let marginLeftParent = parseFloat($(`[id*='answer-${response.parentAnswerId}']`).css('margin-left'));
    let marginLeft = marginLeftParent + 50;

    let reply = `
            <div id="answer-${response.answerId}" class="rounded border p-3 mb-3" style="margin-left: ${marginLeft}px;">
                <div class="d-flex justify-content-between">
                    <p><strong>${response.email}</strong> on ${getFormattedDate()} (0 seconds ago)</p>
                    <div class="d-flex gap-2">
                        <span class="link-text text-danger" onclick="deleteAnswer(${response.AnswerId})">Delete</span>
                        <span class="link-text text-success" onclick="appendInput(${response.questionId}, ${response.answerId}, "edit")">Edit</span>
                        <span class="link-text" onclick="appendInput(${response.questionId}, ${response.answerId})">Reply</span>
                    </div>
                </div>
                <div class="form-group row">
                    <p>${response.explanation}</p>
                </div>
            </div>`;

    $(`[id*='answer-${response.parentAnswerId}']`).after(reply);
}

function deleteQuestion(questionId) {
    $.ajax({
        url: `/question/${questionId}`,
        method: 'DELETE',
        dataType: 'json',
        success: function () {
            toastMessageNext("Question Deleted!", ToastType.Success);
            window.location.href = '/';
        },
        error: function (xhr) {
            errorMessage = xhr.responseText || "Internal server error. Please try again later.";
            toastMessage(errorMessage);
        }
    });
}

function deleteAnswer(answerId) {
    $.ajax({
        url: `/answer/${answerId}`,
        method: 'DELETE',
        success: function (response) {
            toastMessage("Answer Deleted!", ToastType.Success);

            $(`[id*='answer-${answerId}']`).remove();
            $(`[id*='parent-${answerId}']`).remove();
        },
        error: function (xhr) {
            errorMessage = xhr.responseText || "Internal server error. Please try again later.";
            toastMessage(errorMessage);
        }
    });
}