const imageSelector = document.getElementById('imageSelector');
const canvas = document.getElementById('imageCanvas');
const ctx = canvas.getContext('2d');
const image = document.getElementById('image');
const tagNameInput = document.getElementById('tagName');
const annotations = [];

imageSelector.addEventListener('change', function () {
    loadImage();
});

function loadImage() {
    const selectedImageId = imageSelector.value;

    if (!selectedImageId) {
        return;
    }

    // Construct the image URL using string concatenation
    const imageUrl = '/ImageUpload/GetImageById?id=' + selectedImageId;

    image.onload = function () {
        canvas.width = image.width;
        canvas.height = image.height;
        ctx.drawImage(image, 0, 0, image.width, image.height);
        canvas.style.display = 'block';
    };

    image.src = imageUrl;
}

function saveAnnotation() {
    const tagName = tagNameInput.value;

    if (!tagName) {
        console.error('Tag name is required')
        return;
    }
    const boundingBox = getBoundingBox();
    const annotation = { tagName, boundingBox };

    /* Send annotations to server using AJAX */
    fetch('ImageUpload/SaveAnnotation', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(annotation)
    })
    .then(response => {
        if (!response.ok) {
            throw new Error('Failed to save annotation');
        }
    console.log('Annotation saved succesfully');
    })
    .catch (error => {
        console.error(error);
    });
}

function getBoundingBox() {
    return { x: 50, y: 50, width: 100, height: 100 };
}
