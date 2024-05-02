// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
<script>
    document.addEventListener("DOMContentLoaded", function() {
    const allRatings = document.querySelectorAll('.rating');

    allRatings.forEach(rating => {
        const ratingStars = rating.querySelectorAll('input[type="radio"]');
    const allLabels = rating.querySelectorAll('label');

        ratingStars.forEach((star, index) => {
            const correspondingLabel = rating.querySelector(`label[for="${star.id}"]`);

    star.addEventListener('click', function(e) {
                if (correspondingLabel.classList.contains('active') && index === 0) {
        e.preventDefault(); // Impedisce la deselezione della prima stella
                } else {
        // Seleziona solo la stella cliccata, deselezionando le altre
        ratingStars.forEach((s, i) => {
            s.checked = false;
            allLabels[i].classList.remove('active');
        });

    // Reseleziona la stella cliccata
    star.checked = true;
    correspondingLabel.classList.add('active');
                }
            });
        });
    });
});

</script>