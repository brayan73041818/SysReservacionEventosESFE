document.addEventListener('DOMContentLoaded', function () {
    var calendarEl = document.getElementById('calendar');

    var calendar = new FullCalendar.Calendar(calendarEl, {
        initialView: 'dayGridMonth',
        events: '/calendar/events' // Esta es la ruta a tu controlador que devolverá los eventos del calendario
    });

    calendar.render();
});

