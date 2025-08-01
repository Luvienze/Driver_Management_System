async function fetchDailyMaleDriverCount() {
    try {
        const response = await fetch('/Driver/GetDailyMaleDriverCount', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            }
        });
        if (!response.ok) {
            throw new Error(`API error: ${response.status}`);
        }
        const count = await response.json();
        document.getElementById('totalMaleDriversBox').textContent = count;
    } catch (error) {
        console.error('Could not fetch male driver count:', error);
        document.getElementById('totalMaleDriversBox').textContent = 'Error';
    }
}

document.addEventListener("DOMContentLoaded", () => {
    fetchDailyMaleDriverCount();
});