// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener("DOMContentLoaded", function () {
    // Prima parte del codice: gestione del rating
    const allRatings = document.querySelectorAll('.rating');
    allRatings.forEach(rating => {
        const ratingStars = rating.querySelectorAll('input[type="radio"]');
        const allLabels = rating.querySelectorAll('label');

        ratingStars.forEach((star, index) => {
            const correspondingLabel = rating.querySelector(`label[for="${star.id}"]`);

            star.addEventListener('click', function (e) {
                if (correspondingLabel.classList.contains('active') && index === 0) {
                    e.preventDefault(); // Impedisce la deselezione della prima stella
                } else {
                    // Deseleziona tutte le stelle
                    ratingStars.forEach((s, i) => {
                        s.checked = false;
                        allLabels[i].classList.remove('active');
                    });

                    // Seleziona la stella cliccata
                    star.checked = true;
                    correspondingLabel.classList.add('active');
                }
            });
        });
    });

    // Seconda parte del codice: gestione dell'espansione del riquadro
    const expandableItems = document.querySelectorAll(".toggle-expandable");
    expandableItems.forEach(item => {
        item.addEventListener("click", function () {
            var expandableId = item.getAttribute("data-expandable-id");
            var $commentsArea = document.getElementById("expandable-" + expandableId);

            if ($commentsArea.classList.contains("d-none")) {
                $commentsArea.classList.remove("d-none");
                $commentsArea.classList.add("d-block");
            } else {
                $commentsArea.classList.remove("d-block");
                $commentsArea.classList.add("d-none");
            }
        });
    });
});
