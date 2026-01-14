// Sayfa içerikleri verisi (HTML Şablonları)
const contentData = {
    dashboard: `
        <div class="d-flex justify-content-between align-items-center mb-4">
            <div>
                <h2 class="fw-800 mb-0">Hoş geldin, Ahmet! 👋</h2>
                <p class="text-muted mb-0">İşte bugünkü kullanım verilerin.</p>
            </div>
            <div class="d-flex gap-2">
                <button class="btn btn-white border shadow-sm rounded-circle p-2"><span class="material-symbols-outlined">notifications</span></button>
                <button class="btn btn-white border shadow-sm rounded-circle p-2"><span class="material-symbols-outlined">settings</span></button>
            </div>
        </div>

        <div class="row g-4 mb-4" style="min-height: 200px;">
            <div class="col-lg-8">
                <div class="card plan-main-card p-4 p-md-5 h-100 shadow-lg border-0" 
                     role="button" 
                     onclick="loadContent('plans')" 
                     style="cursor: pointer; transition: all 0.3s ease; user-select: none;">
                    <div class="row align-items-center h-100">
                        <div class="col-md-7">
                            <span class="badge bg-white text-primary rounded-pill px-3 py-2 mb-3 fw-bold text-xs">AKTİF</span>
                            <h2 class="fw-800 mb-1">Premium Business Plan</h2>
                            <p class="opacity-75 mb-0">Sınırsız konuşma/SMS + 50GB yüksek hız.</p>
                        </div>
                        <div class="col-md-5 text-md-end">
                            <h1 class="fw-800 mb-0">20GB<small class="fs-6 opacity-75">/ay</small></h1>
                            <p class="text-xs opacity-75 mt-2">Detaylar için tıkla <span class="material-symbols-outlined fs-6 align-middle">chevron_right</span></p>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-4">
                <div class="card glass-card h-100 p-4">
                    <h6 class="fw-bold text-secondary d-flex align-items-center mb-4">
                        <span class="material-symbols-outlined me-2 text-primary">analytics</span> Kullanım Trendi
                    </h6>
                    <div class="mt-auto">
                        <div class="d-flex align-items-end gap-2">
                            <h2 class="fw-800 mb-0">45GB</h2>
                            <span class="badge bg-success-subtle text-success fw-bold text-xs">+15% ↑</span>
                        </div>
                        <p class="text-muted text-xs mt-1">Geçen aya göre (39GB)</p>
                        <div class="progress mt-3" style="height: 8px;">
                            <div class="progress-bar bg-primary rounded-pill" style="width: 85%"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row g-4 mb-4 text-center">
            <div class="col-md-4"><div class="card glass-card p-4"><div class="usage-circle" style="border-top-color: #06b6d4; color: #06b6d4;">75%</div><h6 class="fw-bold mb-1">Veri</h6><p class="text-muted text-xs mb-0">15GB / 20GB Kullanıldı</p></div></div>
            <div class="col-md-4"><div class="card glass-card p-4"><div class="usage-circle" style="border-top-color: #7c3aed; color: #7c3aed;">40%</div><h6 class="fw-bold mb-1">Konuşma</h6><p class="text-muted text-xs mb-0">400 / 1000 Dakika</p></div></div>
            <div class="col-md-4"><div class="card glass-card p-4"><div class="usage-circle" style="border-top-color: #10b981; color: #10b981;">10%</div><h6 class="fw-bold mb-1">Mesaj</h6><p class="text-muted text-xs mb-0">Sınırsız Paket</p></div></div>
        </div>

        <div class="mt-auto">
            <h6 class="fw-bold mb-3 text-secondary d-flex align-items-center px-1">
                <span class="material-symbols-outlined me-2 fs-5">auto_awesome</span> Senin İçin Öneriler
            </h6>
            <div class="row g-3">
                <div class="col-md-4"><div class="card glass-card promo-card"><div class="p-3 d-flex align-items-center gap-3"><div class="bg-primary-subtle p-3 rounded-4 text-primary"><span class="material-symbols-outlined fs-3">add_to_drive</span></div><div class="flex-grow-1 overflow-hidden"><h6 class="fw-bold mb-0 text-truncate">5GB Ek Veri</h6><button class="btn btn-primary btn-sm rounded-pill w-100 mt-2 fw-bold text-xs">$5.00 • Ekle</button></div></div></div></div>
                <div class="col-md-4"><div class="card glass-card promo-card"><div class="p-3 d-flex align-items-center gap-3"><div class="bg-warning-subtle p-3 rounded-4 text-warning"><span class="material-symbols-outlined fs-3">public</span></div><div class="flex-grow-1 overflow-hidden"><h6 class="fw-bold mb-0 text-truncate">Yurt Dışı Paketi</h6><button class="btn btn-outline-dark btn-sm rounded-pill w-100 mt-2 fw-bold text-xs">İncele</button></div></div></div></div>
                <div class="col-md-4"><div class="card glass-card promo-card"><div class="p-3 d-flex align-items-center gap-3"><div class="bg-success-subtle p-3 rounded-4 text-success"><span class="material-symbols-outlined fs-3">group</span></div><div class="flex-grow-1 overflow-hidden"><h6 class="fw-bold mb-0 text-truncate">Aile İndirimi</h6><button class="btn btn-outline-success btn-sm rounded-pill w-100 mt-2 fw-bold text-xs">Sorgula</button></div></div></div></div>
            </div>
        </div>
    `,
    plans: `
        <h2 class="fw-800 mb-4">Abonelik Planlarım</h2>
        <div class="glass-card p-4 mb-4">
            <div class="d-flex justify-content-between align-items-center mb-3">
                <h5 class="fw-bold mb-0">Premium Business Plan (Aktif)</h5>
                <span class="text-success fw-bold">$49.99/ay</span>
            </div>
            <p class="text-muted">Bu plan 24 Ekim 2023 tarihinde yenilenecektir. Otomatik ödeme tanımlıdır.</p>
            <button class="btn btn-outline-danger btn-sm rounded-pill">İptal Et</button>
        </div>
    `,
    usage: `
        <h2 class="fw-800 mb-4">Detaylı Kullanım Kayıtları</h2>
        <div class="glass-card p-4">
            <table class="table table-hover">
                <thead><tr><th>Tarih</th><th>Servis</th><th>Miktar</th></tr></thead>
                <tbody>
                    <tr><td>15.01.2026</td><td>Veri</td><td>1.2 GB</td></tr>
                    <tr><td>14.01.2026</td><td>Konuşma</td><td>45 dk</td></tr>
                    <tr><td>13.01.2026</td><td>Veri</td><td>850 MB</td></tr>
                </tbody>
            </table>
        </div>
    `
};

/**
 * Belirtilen sayfayı yükler ve navigasyonu günceller.
 * @param {string} pageId - Yüklenecek sayfanın anahtarı (dashboard, plans, usage)
 */
function loadContent(pageId) {
    const mainContent = document.getElementById('main-content');
    if (!mainContent || !contentData[pageId]) return;
    
    // Geçiş efekti: Opaklığı azalt
    mainContent.style.opacity = '0';
    
    setTimeout(() => {
        // İçeriği değiştir
        mainContent.innerHTML = contentData[pageId];
        
        // Geçiş efekti: Opaklığı artır
        mainContent.style.opacity = '1';

        // Navigasyon menüsündeki 'active' sınıfını güncelle
        const links = document.querySelectorAll('.nav-link');
        links.forEach(link => {
            // Veri anahtarına göre veya text içeriğine göre kontrol
            const isMatch = link.getAttribute('onclick')?.includes(pageId) || 
                            link.textContent.toLowerCase().includes(pageId.toLowerCase());
            
            link.classList.toggle('active', isMatch);
        });
    }, 150);
}

// Sayfa ilk yüklendiğinde Dashboard'u getir
document.addEventListener('DOMContentLoaded', () => {
    loadContent('dashboard');
});