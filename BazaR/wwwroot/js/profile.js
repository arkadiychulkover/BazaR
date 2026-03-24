// ─── CSRF токен ─────────────────────────────────────────────────────────────
let token;

// ─── Акордеон ───────────────────────────────────────────────────────────────
document.addEventListener('DOMContentLoaded', function () {
    // Читаємо токен після завантаження DOM
    token = document.querySelector('input[name="__RequestVerificationToken"]')?.value;
    if (!token) console.error('CSRF token not found!');

    const blocks = document.querySelectorAll('.profile-block');
    blocks.forEach(block => {
        const button = block.querySelector('.profile-block__head');
        const panel = document.getElementById(button.getAttribute('aria-controls'));
        button.addEventListener('click', function () {
            const isOpen = button.getAttribute('aria-expanded') === 'true';
            button.setAttribute('aria-expanded', String(!isOpen));
            block.classList.toggle('is-open', !isOpen);
            if (panel) panel.hidden = isOpen;
        });
    });
});

// ─── Редагування особистих полів ────────────────────────────────────────────
function startEdit(field, type) {
    const valEl = document.getElementById('val-' + field);
    if (!valEl || valEl.querySelector('input, select')) return;

    const currentText = valEl.textContent.trim();
    const currentVal = currentText === 'Не вказано' ? '' : currentText;
    let input;

    if (type === 'select') {
        input = document.createElement('select');
        [['', '— Оберіть —'], ['Чоловіча', 'Чоловіча'], ['Жіноча', 'Жіноча'], ['Інша', 'Інша']]
            .forEach(([v, l]) => {
                const opt = document.createElement('option');
                opt.value = v;
                opt.textContent = l;
                if (v === currentVal) opt.selected = true;
                input.appendChild(opt);
            });
    } else {
        input = document.createElement('input');
        input.type = type === 'date' ? 'date' : 'text';
        if (type === 'date' && currentVal) {
            const parts = currentVal.split('.');
            if (parts.length === 3) input.value = `${parts[2]}-${parts[1]}-${parts[0]}`;
        } else {
            input.value = currentVal;
        }
    }

    input.className = 'profile-inline-input';

    const saveBtn = document.createElement('button');
    saveBtn.type = 'button';
    saveBtn.textContent = 'Зберегти';
    saveBtn.className = 'profile-save-btn';

    const cancelBtn = document.createElement('button');
    cancelBtn.type = 'button';
    cancelBtn.textContent = 'Скасувати';
    cancelBtn.className = 'profile-cancel-btn';

    valEl.innerHTML = '';
    valEl.appendChild(input);
    valEl.appendChild(saveBtn);
    valEl.appendChild(cancelBtn);
    input.focus();

    cancelBtn.addEventListener('click', () => {
        valEl.textContent = currentText;
    });

    saveBtn.addEventListener('click', async () => {
        let value = input.value.trim();
        let displayValue = value || 'Не вказано';

        if (type === 'date' && value) {
            const [y, m, d] = value.split('-');
            displayValue = `${d}.${m}.${y}`;
        }

        try {
            const res = await fetch('/Profile/UpdatePersonalField', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': token
                },
                body: JSON.stringify({ field, value: value || null })
            });

            const text = await res.text();
            console.log('Status:', res.status, '| Response:', text);

            if (!res.ok) throw new Error(`${res.status}: ${text}`);

            valEl.textContent = displayValue;
        } catch (err) {
            console.error('Save error:', err.message);
            alert('Не вдалося зберегти. Спробуйте ще раз.');
            valEl.textContent = currentText;
        }
    });
}

// ─── Показ форм додавання ────────────────────────────────────────────────────
function showAddRecipientForm() {
    document.getElementById('add-recipient-form').style.display = 'block';
}
function openPromoModal() {
    document.getElementById('add-promotion-form').style.display = 'block';
}
function closePromoModal() {
    document.getElementById('add-promotion-form').style.display = 'none';
}
function showAddAddressForm() {
    document.getElementById('add-address-form').style.display = 'block';
}
function showAddPetForm() {
    document.getElementById('add-pet-form').style.display = 'block';
}
function showAddInfoForm() {
    document.getElementById('add-info-form').style.display = 'block';
}

// ─── Збереження отримувача ───────────────────────────────────────────────────
async function saveRecipient() {
    const dto = {
        firstName: document.getElementById('rec-firstName').value.trim(),
        lastName: document.getElementById('rec-lastName').value.trim(),
        middleName: document.getElementById('rec-middleName').value.trim() || null,
        phone: document.getElementById('rec-phone').value.trim() || null
    };
    if (!dto.firstName || !dto.lastName) {
        alert("Ім'я та призвіще обов'язкові");
        return;
    }
    try {
        const res = await fetch('/Profile/AddRecipient', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json', 'RequestVerificationToken': token },
            body: JSON.stringify(dto)
        });

        const text = await res.text();
        console.log('AddRecipient Status:', res.status, '| Response:', text);

        if (!res.ok) throw new Error(`${res.status}: ${text}`);
        location.reload();
    } catch (err) {
        console.error('SaveRecipient error:', err.message);
        alert('Помилка збереження. Спробуйте ще раз.');
    }
}

// ─── Збереження адреси ───────────────────────────────────────────────────────
async function saveAddress() {
    const dto = {
        city: document.getElementById('addr-city').value.trim(),
        street: document.getElementById('addr-street').value.trim(),
        building: document.getElementById('addr-building').value.trim() || null,
        apartment: document.getElementById('addr-apartment').value.trim() || null,
        postalCode: document.getElementById('addr-postal').value.trim() || null
    };
    if (!dto.city || !dto.street) {
        alert("Місто та вулиця обов'язкові");
        return;
    }
    try {
        const res = await fetch('/Profile/AddDeliveryAddress', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json', 'RequestVerificationToken': token },
            body: JSON.stringify(dto)
        });

        const text = await res.text();
        console.log('AddDeliveryAddress Status:', res.status, '| Response:', text);

        if (!res.ok) throw new Error(`${res.status}: ${text}`);
        location.reload();
    } catch (err) {
        console.error('SaveAddress error:', err.message);
        alert('Помилка збереження. Спробуйте ще раз.');
    }
}

// ─── Збереження тварини ──────────────────────────────────────────────────────
async function savePet() {
    const dto = {
        name: document.getElementById('pet-name').value.trim(),
        type: document.getElementById('pet-type').value.trim() || null,
        breed: document.getElementById('pet-breed').value.trim() || null
    };
    if (!dto.name) {
        alert("Кличка обов'язкова");
        return;
    }
    try {
        const res = await fetch('/Profile/AddPet', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json', 'RequestVerificationToken': token },
            body: JSON.stringify(dto)
        });

        const text = await res.text();
        console.log('AddPet Status:', res.status, '| Response:', text);

        if (!res.ok) throw new Error(`${res.status}: ${text}`);
        location.reload();
    } catch (err) {
        console.error('SavePet error:', err.message);
        alert('Помилка збереження. Спробуйте ще раз.');
    }
}

// ─── Збереження додаткової інформації ───────────────────────────────────────
async function saveAdditionalInfo() {
    const dto = {
        key: document.getElementById('info-key').value.trim(),
        value: document.getElementById('info-value').value.trim() || null
    };
    if (!dto.key) {
        alert("Назва обов'язкова");
        return;
    }
    try {
        const res = await fetch('/Profile/SaveAdditionalInfo', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json', 'RequestVerificationToken': token },
            body: JSON.stringify(dto)
        });

        const text = await res.text();
        console.log('SaveAdditionalInfo Status:', res.status, '| Response:', text);

        if (!res.ok) throw new Error(`${res.status}: ${text}`);
        location.reload();
    } catch (err) {
        console.error('SaveAdditionalInfo error:', err.message);
        alert('Помилка збереження. Спробуйте ще раз.');
    }
}

// ─── Видалення елементів ─────────────────────────────────────────────────────
// type: 'recipient' | 'address' | 'pet' | 'additionalinfo'
async function deleteItem(type, id, btn) {
    if (!confirm('Ви впевнені?')) return;

    const urlMap = {
        recipient: '/Profile/DeleteRecipient',
        address: '/Profile/DeleteDeliveryAddress',
        pet: '/Profile/DeletePet',
        additionalinfo: '/Profile/DeleteAdditionalInfo'
    };

    try {
        const res = await fetch(urlMap[type], {
            method: 'POST',
            headers: { 'Content-Type': 'application/json', 'RequestVerificationToken': token },
            body: JSON.stringify(id)
        });

        const text = await res.text();
        console.log('DeleteItem Status:', res.status, '| Response:', text);

        if (!res.ok) throw new Error(`${res.status}: ${text}`);

        // Видаляємо картку з DOM без перезавантаження
        btn.closest('.profile-info-item').remove();
    } catch (err) {
        console.error('DeleteItem error:', err.message);
        alert('Помилка видалення. Спробуйте ще раз.');
    }
}