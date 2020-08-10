window.copyTextToClipboard = (text) => {
    navigator.permissions.query({ name: "clipboard-write" }).then(result => {
        if (result.state == "granted" || result.state == "prompt") {
            try {
                navigator.clipboard.writeText(text).then(() => { }, () => { });
            } catch (e) {
                console.log("Exception thrown while trying to copy to clipboard. " + e);
            }
        } else {
            console.log("The browser doesn't have access right to copy to clipboard.");
        }
    });
}