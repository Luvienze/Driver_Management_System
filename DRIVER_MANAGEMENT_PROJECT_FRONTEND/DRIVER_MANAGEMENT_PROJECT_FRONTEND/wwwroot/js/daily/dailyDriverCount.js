async function fetchDailyDriverCount() {
    try {
        const response = await fetch('/Task/GetDailyDriverCount', {
            method: 'POST', 
            headers: {
                'Content-Type': 'application/json'
            }
        });
        if (!response.ok) {
            throw new Error(`API hatası: ${response.status}`);
        }
        const count = await response.json();
        document.getElementById('dailyDriverCountBox').textContent = count;
    } catch (error) {
        console.error('Failed at fetch task count data:', error);
        document.getElementById('dailyDriverCountBox').textContent = 'Error';
    }
}

document.addEventListener("DOMContentLoaded", () => {
    fetchDailyDriverCount();
});