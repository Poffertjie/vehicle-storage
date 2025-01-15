function OpenDocument(url) {
    fetch(url)
        .then(res => res.blob())
        .then(URL.createObjectURL)
        .then(window.open)
}