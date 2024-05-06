// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener("DOMContentLoaded", function () {
    //Gestione del rating
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

    const expandableElements = document.querySelectorAll(".expandable");

    expandableElements.forEach(element => {
        element.addEventListener("click", function () {
            const expandableId = this.getAttribute("data-expandable-id");
            const content = document.querySelector(`#expandable-content-${expandableId}`);

            content.classList.toggle("d-none"); // Alterna la visibilità della parte espandibile
        });
    });

    const addCommentButtons = document.querySelectorAll(".add-comment-btn");

    addCommentButtons.forEach(button => {
        button.addEventListener("click", function (event) {
            event.stopPropagation(); // Impedisce la chiusura della parte espandibile

            const targetId = this.getAttribute("data-target");
            const commentForm = document.querySelector(targetId);

            if (commentForm.classList.contains("d-none")) {
                commentForm.classList.remove("d-none"); // Mostra la textarea e il pulsante "Invia commento"
            }
        });
    });

    // Assicurati che il clic sulla textarea e sul pulsante non chiuda la parte espandibile
    const textareas = document.querySelectorAll("textarea");
    textareas.forEach(textarea => {
        textarea.addEventListener("click", function (event) {
            event.stopPropagation(); // Evita la chiusura del riquadro
        });
    });

    const submitButtons = document.querySelectorAll(".btn-primary.mt-2");
    submitButtons.forEach(button => {
        button.addEventListener("click", function (event) {
            event.stopPropagation(); // Assicura che il clic sul pulsante non chiuda la parte espandibile
        });
    });

    // Gestione dell'evento di clic sul pulsante
    const addNewItemButton = document.getElementById("add-new-item");
    if (addNewItemButton) {
        addNewItemButton.addEventListener("click", function (e) {
            e.preventDefault;
            // Struttura del nuovo riquadro
            const newBox = `
                <div class="custom-box p-3 mb-3 toggle-expandable">
                    <div class="row align-items-center">
                        <div class="col-md-4 col-sm-6 text-center">
                            <input type="file" accept="image/*" class="form-control" placeholder="Carica un'immagine" /> <!-- Carica immagine -->
                        </div>
                        <div class="col-md-8 col-sm-6">
                            <div class="d-flex flex-column align-items-start">
                                <input tipo="text" class="form-control" placeholder="Titolo" /> <!-- Input titolo -->
                                <input tipo="text" class="form-control mt-2" placeholder="Prezzo ($)" /> <!-- Input prezzo -->
                                <input tipo="date" class="form-control mt-2" placeholder="Data" /> <!-- Input data -->
                                <div class="info">
                                    <div class="rating">
                                        <input type="radio" id="star5_@i" name="rating_@i" value="5" />
                                        <label for="star5_@i" title="5 stelle"></label>
                                        <input type="radio" id="star4_@i" name="rating_@i" value="4" />
                                        <label for="star4_@i" title="4 stelle"></label>
                                        <input type="radio" id="star3_@i" name="rating_@i" value="3" />
                                        <label for="star3_@i" title="3 stelle"></label>
                                        <input type="radio" id="star2_@i" name="rating_@i" value="2" />
                                        <label for="star2_@i" title="2 stelle"></label>
                                        <input tipo="radio" id="star1_@i" name="rating_@i" value="1" />
                                        <label for="star1_@i" title="1 stella"></label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="comments-area d-none">
                        <h3>Commenti:</h3>
                        <p>Inserisci i tuoi commenti qui...</p>
                    </div>
                    <!-- Pulsante di pubblicazione -->
                    <div class="text-center mt-3">
                        <button class="btn btn-primary" id="publish-new-box">Pubblica</button> <!-- Pulsante di pubblicazione -->
                    </div>
                </div>
            `;

            const contentArea = document.getElementById("content-area");

            if (contentArea) {
                contentArea.insertAdjacentHTML("beforeend", newBox); // Aggiunge il nuovo riquadro
            }

            // Funzione per pubblicare il nuovo riquadro
            const publishButton = document.getElementById("publish-new-box");

            if (publishButton) {
                publishButton.addEventListener("click", function () {
                    console.log("Riquadro pubblicato"); // Test per verificare che l'evento funzioni
                });
            }
        });
    }
});



