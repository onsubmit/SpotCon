"use strict";
window.onload = function () {
	require(['$api/models'], function (models) {
		models.application.addEventListener('dropped', function () {
			var playlist = models.Playlist.fromURI(models.application.dropped);
			playlist.load('name', 'tracks').done(function (loaded) {
				var text = loaded.name + '|||'
				loaded.tracks.snapshot().done(function (snapshot) {
					var tracks = snapshot.toURIs();

					for (var i = 0; i < tracks.length; i++) {
						text += tracks[i].replace('spotify:track:', '') + ','
					}

					$.post("http://spotconws.com:8080/SpotConService.asmx/Set", {
						value : text
					});
					
					$("#status").html("All done transferring " + tracks.length + " tracks!");
				});
			});
		});
	});
}