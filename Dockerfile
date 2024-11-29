FROM archlinux
EXPOSE 443

RUN pacman -Syu --noconfirm unzip
RUN curl -fsSL https://bun.sh/install | bash
ENV PATH=$PATH:/root/.bun/bin

COPY ./srv /srv
WORKDIR /srv

CMD ["sh","/srv/start.sh"]
