// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
<script>
    document.addEventListener("DOMContentLoaded", function() {
        const ratingStars = document.querySelectorAll('.rating input');
    const publishButton = document.querySelector('#publishButton');
    const ratingValueField = document.querySelector('#ratingValue');

    let selectedRating = null;

        ratingStars.forEach(star => {
        star.addEventListener('click', function () {
            selectedRating = this.value;
        });
        });

    publishButton.addEventListener('click', function() {
        ratingValueField.value = selectedRating;
        });
    });
</script>