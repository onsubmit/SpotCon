"use strict";
window.onload = function () {
    require(['$api/models'], function (models) {
        var dropBox = document.querySelector('#drop-box');

        dropBox.addEventListener('dragstart', function (e) {
            e.dataTransfer.setData('text/html', this.innerHTML);
            e.dataTransfer.effectAllowed = 'copy';
        }, false);

        dropBox.addEventListener('dragenter', function (e) {
            e.preventDefault();
            e.dataTransfer.dropEffect = 'copy';
            this.classList.add('over');
        }, false);

        dropBox.addEventListener('dragover', function (e) {
            e.preventDefault();
            e.dataTransfer.dropEffect = 'copy';
            return false;
        }, false);

        dropBox.addEventListener('dragleave', function (e) {
            e.preventDefault();
            this.classList.remove('over');
        }, false);

        dropBox.addEventListener('drop', function (e) {
            e.preventDefault();
            var uri = e.dataTransfer.getData('text');

            var playlist = models.Playlist.fromURI(uri);
            playlist.load('name', 'tracks').done(function (loaded) {
                var name = loaded.name;
                if (playlist.uri.endsWith('/starred')) {
                    name = 'Starred';
                }

                loaded.tracks.snapshot().done(function (snapshot) {
                    var tracks = snapshot.toURIs();

                    var text = '';
                    for (var i = 0; i < tracks.length; i++) {
                        text += tracks[i].replace('spotify:track:', '') + ','
                    }

                    $.post("http://localhost:17290/SpotConService.asmx/SetPlaylist", {
                        uri: uri,
                        name: name,
                        tracks: text
                    });

                    $("#status").html("All done transferring " + tracks.length + " tracks! Please return to the SpotCon client and press OK to complete the import.");
                });
            });
        }, false);
	});
}

String.prototype.endsWith = function(suffix) {
    return this.indexOf(suffix, this.length - suffix.length) !== -1;
};