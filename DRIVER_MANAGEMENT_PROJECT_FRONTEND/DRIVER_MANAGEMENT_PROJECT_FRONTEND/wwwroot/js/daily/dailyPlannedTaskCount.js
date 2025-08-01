async function fetchDailyPlannedTaskCount() {
    try {
        const response = await fetch('/Task/GetDailyPlannedTaskCount', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            }
        });
        if (!response.ok) {
            throw new Error(`API error: ${response.status}`);
        }
        const count = await response.json();
        document.getElementById('dailyPlannedTaskCount').textContent = count; 
    } catch (error) {
        console.error('Could not fetch planned task count data:', error);
        document.getElementById('dailyPlannedTaskCount').textContent = 'Error'; 
    }
}

document.addEventListener("DOMContentLoaded", () => {
    fetchDailyPlannedTaskCount(); 
});