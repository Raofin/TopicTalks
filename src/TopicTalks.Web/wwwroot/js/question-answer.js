let selectedId = null;
let inputAppeded = false;

function appendInput(questionId, answerId, edit) {
    if (selectedId != answerId) {
        selectedId = answerId;
        closeReplyInput();
        inputAppeded = false;
    }
    if (inputAppeded) {
        closeReplyInput();
        inputAppeded = false;
    } else {
        $(`[id*='answer-${answerId}']:first`).append(`
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
                .done((data) => { $("#reply-text").val(data.explanation); })
                .fail(() => { toastMessage("Error fetching answer.", ToastColor.Red); });
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
            submitHandler: (form) => {
                if (edit) {
                    $.ajax({
                        url: '/answer',
                        type: 'PATCH',
                        data: $(form).serialize(),
                        success: (data) => {
                            $("#explanation-" + data.answerId).text(data.explanation);
                            toastMessage("Edit Successful!", ToastColor.Green);
                            closeReplyInput();
                        },
                        error: () => {
                            toastMessage('Internal server error. Please try again later.', ToastColor.Red);
                        }
                    });

                    return;
                }

                $.ajax({
                    url: '/answer',
                    type: 'POST',
                    data: $(form).serialize(),
                    success: (response) => {
                        appendReply(response);
                        toastMessage("Reply Submitted!", ToastColor.Green);
                        closeReplyInput();
                    },
                    error: () => {
                        toastMessage('Internal server error. Please try again later.', ToastColor.Red);
                    }
                });
            }
        });

        inputAppeded = true;
    }
}

function fetchAnswer(answerId) {
    $.ajax({
        url: '/answer/' + answerId,
        type: 'GET',
        success: () => {
            $("#answer-23 textarea#reply-text").val((_index, value) => value + explanation);
        },
        error: () => {
            toastMessage("Error fetching answer.", ToastColor.Red);
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
    submitHandler: (form) => {
        $.ajax({
            url: '/answer',
            type: 'POST',
            data: $(form).serialize(),
            success: (response) => {
                appendAnswer(response);
                toastMessage("Answer Submitted!", ToastColor.Green);
                $("#answer-form")[0].reset();
            },
            error: () => {
                toastMessage('Internal server error. Please try again later.', ToastColor.Red);
            }
        });
    }
})

function closeReplyInput() {
    selectedId = null;
    $("#reply-input").remove();
}

function appendAnswer(response) {
    console.table(response)
    var answer = `
        <div id="answer-${response.answerId} parent-${response.parentAnswerId}" class="rounded border p-3 mb-3">
            <div class="d-flex justify-content-between">
                <div class="d-flex">
                    <span class="user-image" style="margin-right: 0.3rem">
                        <img src="${response.userInfo?.profileImageUrl ?? '/img/user.svg'}" alt="" />
                    </span>
                    <p><strong>${response.userInfo.username}</strong> on ${currentDate()} (just now)</p>
                </div>
                <div class="d-flex gap-2">
                    <span class="link text-danger" onclick="deleteAnswer(${response.answerId})" pop="Delete" data-tippy-theme="red">
                        <i class="fi i-delete" style="display: inline;"></i>
                    </span>
                    <span class="link text-success" onclick="appendInput(${response.questionId}, ${response.answerId}, true)" pop="Edit">
                        <i class="fi i-edit-2" style="display: inline;"></i>
                    </span>
                    <span class="link" onclick="appendInput(${response.questionId}, ${response.answerId})" pop="Reply">
                        <i class="fi i-reply blue-hover" style="color: #333; display: inline;"></i>
                    </span>
                </div>
            </div>
            <div class="form-group row">
                <p id="explanation-${response.answerId}">${response.explanation}</p>
            </div>
        </div>`;

    $("#answer-container").append(answer);

    setTippyContent()
}

function appendReply(response) {
    let marginLeftParent = parseFloat($(`[id*='answer-${response.parentAnswerId}']`).css('margin-left'));
    let marginLeft = marginLeftParent + 50;

    let reply = `
            <div id="answer-${response.answerId} parent-${response.parentAnswerId}" class="rounded border p-3 mb-3" style="margin-left: ${marginLeft}px;">
                <div class="d-flex justify-content-between">
                    <div class="d-flex">
                        <span class="user-image" style="margin-right: 0.3rem">
                            <img src="${response.userInfo?.profileImageUrl ?? '/img/user.svg'}" alt="" />
                        </span>
                        <p><strong>${response.userInfo.username}</strong> on ${currentDate()} (just now)</p>
                    </div>
                    <div class="d-flex gap-2">
                        <span class="link text-danger" onclick="deleteAnswer(${response.answerId})" pop="Delete" data-tippy-theme="red">
                            <i class="fi i-delete" style="display: inline;"></i>
                        </span>
                        <span class="link text-success" onclick="appendInput(${response.questionId}, ${response.answerId}, true)" pop="Edit">
                            <i class="fi i-edit-2" style="display: inline;"></i>
                        </span>
                        <span class="link" onclick="appendInput(${response.questionId}, ${response.answerId})" pop="Reply">
                            <i class="fi i-reply blue-hover" style="color: #333; display: inline;"></i>
                        </span>
                    </div>
                </div>
                <div class="form-group row">
                    <p id="explanation-${response.answerId}">${response.explanation}</p>
                </div>
            </div>`;

    $(`[id*='answer-${response.parentAnswerId}']`).after(reply);

    setTippyContent()
}

function deleteQuestion(questionId) {
    $.ajax({
        url: `/question/${questionId}`,
        method: 'DELETE',
        success: () => {
            toastMessageNext("Question Deleted!", ToastColor.Green);
            window.location.href = '/';
        },
        error: () => {
            toastMessage('Internal server error. Please try again later.', ToastColor.Red);
        }
    });
}

function deleteAnswer(answerId) {
    $.ajax({
        url: `/answer/${answerId}`,
        method: 'DELETE',
        success: () => {
            toastMessage("Answer Deleted!", ToastColor.Green);

            $(`[id*='answer-${answerId}']`).remove();
            $(`[id*='parent-${answerId}']`).remove();
        },
        error: () => {
            toastMessage('Internal server error. Please try again later.', ToastColor.Red);
        }
    });
}