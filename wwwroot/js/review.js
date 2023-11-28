$(function () {
});

function postReview() {
    let $form = $('#reviewForm');
    let antiForgeryToken = $form.find('input[type="hidden"][name="__RequestVerificationToken"]').val();
    let product = $form.find('input[type="hidden"][name="productID"]').val();
    let reviewText = $form.find('textarea[name="reviewText"]').val();
    let rating = $form.find('input[type="radio"][name="rating"]:checked').val();

    if (!reviewText || !rating) {
        alert('Review Text and Rating are required...')
    } else {
        console.log(antiForgeryToken);
        $.ajax(
            `/api/product/${product}/reviews`,
            {
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': antiForgeryToken,
                    'test-header': antiForgeryToken
                },
                type: 'post',
                data: JSON.stringify(
                    {
                        "Rating": rating,
                        "ReviewText": reviewText
                    }),
                success: (response, textStatus, jqXhr) => {
                    console.log(response, textStatus, jqXhr);
                    window.location.href = `/Product/Reviews/${product}`;
                },
                error: (jqXHR, textStatus, errorThrown) => {
                    alert("Something went wrong, if you know what you're doing, check the console.")
                    console.log(jqXHR, textStatus, errorThrown);
                }
            });
    }
}