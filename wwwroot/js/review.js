$(function () {
});

function postReview() {
    let $form = $('#reviewForm');
    let product = $form.find('input[type="text"][name="productID"]').val();
    let reviewText = $form.find('textarea[name="reviewText"]').val();
    let rating = $form.find('input[type="radio"][name="rating"]:checked').val();

    console.log(product);
    
    if (!reviewText || !rating) {
        alert('TODO: Change this!!!')
    } else {
        $.post(
            `/api/product/${product}/reviews`,
            {
                id: product,
                review: {
                    Rating: rating,
                    ReviewText: reviewText
                }
            },
            (response) => {
                console.log(response);
            }
        )
    }
}